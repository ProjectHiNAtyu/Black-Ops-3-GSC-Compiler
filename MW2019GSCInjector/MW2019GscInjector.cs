using MW2019GSCInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simple_PC_GSC_Injector
{
    public partial class MW2019GscInjector : Form
    {


        [DllImport( "kernel32.dll" )]
        public static extern IntPtr OpenProcess( int dwDesiredAccess , bool bInheritHandle , int dwProcessId );

        [DllImport( "kernel32.dll" )]
        public static extern bool ReadProcessMemory( int hProcess , long lpBaseAddress , byte[] lpBuffer , int dwSize , out int lpNumberOfBytesRead );

        [DllImport( "kernel32.dll" )] //for byte pointers not arrays
        unsafe public static extern bool ReadProcessMemory( int hProcess , long lpBaseAddress , byte* lpBuffer , int dwSize , out int lpNumberOfBytesRead );

        [DllImport( "kernel32.dll" , SetLastError = true )]
        static unsafe extern bool WriteProcessMemory( int hProcess , long lpBaseAddress , byte** lpBuffer , int dwSize , out int lpNumberOfBytesWritten );

        [DllImport( "kernel32.dll" , SetLastError = true , ExactSpelling = true )]
        static extern IntPtr VirtualAllocEx( IntPtr hProcess , IntPtr lpAddress , uint dwSize , uint flAllocationType , uint flProtect );

        unsafe public struct ScriptParseTree
        {
            public char* name;
            public int len;
            public int unk;
            public char* buffer;
        };
        unsafe public struct XAssetEntry
        {
            public int type;
            public int unk1;
            public long* HeaderPointer;
            public byte zoneindex;
            public byte inuse;
            public short unk2;
            public int nextHash;
            public int nextOverride;
            public int unk3;
        };

        IntPtr processBAddr = (IntPtr)0;
        IntPtr processHandle = (IntPtr)0;

        IntPtr memIntPtr;


        const int PROCESS_WM_READ = 0x0010;
        const int PROCESS_VM_WRITE = 0x0020;
        const int PROCESS_VM_OPERATION = 0x0008;

        const uint MEM_COMMIT = 0x00001000;
        const uint MEM_RESERVE = 0x00002000;
        const uint PAGE_READWRITE = 4;



        byte[] ReadBytes( long address , int numBytes )
        {
            // Resulting buffer
            byte[] buffer = new byte[numBytes];
            // Request ReadProcessMemory
            ReadProcessMemory( (int)processHandle , address , buffer , buffer.Length , out int bytesRead );
            // Return result
            return buffer;
        }

        unsafe T[] ReadArrayUnsafe<T>( long address , int count ) where T : unmanaged
        {
            var buffer = ReadBytes( address , count * sizeof( T ) );
            var result = new T[count];

            fixed ( byte* a = buffer )
            fixed ( T* b = result )
                Buffer.MemoryCopy( a , b , buffer.Length , buffer.Length );

            return result;
        }

        unsafe string ReadNullTerminatedString( long address , int bufferSize = 512 )
        {
            var result = stackalloc byte[bufferSize];
            ReadProcessMemory( (int)processHandle , address , result , bufferSize , out int bytesRead );
            int sizeOf;
            for ( sizeOf = 0; sizeOf < bufferSize; sizeOf++ )
            {
                if ( result[sizeOf] == 0x0 )
                    break;
            }
            return Encoding.ASCII.GetString( result , sizeOf );
        }







        bool UpdateProcessInfo( )
        {
            try
            {
                Process process;
                process = Process.GetProcessesByName( textboxProcessName.Text )[0];
                processBAddr = process.MainModule.BaseAddress;
                processHandle = OpenProcess( PROCESS_WM_READ | PROCESS_VM_WRITE | PROCESS_VM_OPERATION , true , process.Id );
                return true;
            }
            catch ( IndexOutOfRangeException )
            {
                // The process isn't currently running
                return false;
            }
        }



        public unsafe void InjectScriptV2( string GSCPath )
        {
            if ( !UpdateProcessInfo( ) )
            {
                AddLog( "MW2019 is not running!" );
                return;
            }

            FileStream script = new FileStream( GSCPath , FileMode.Open );
            byte[] vars = new byte[100000];
            script.Read( vars , 0 , sizeof( int ) * 3 );
            script.Close( );

            byte[] InjectedScript = File.ReadAllBytes( GSCPath );

            memIntPtr = Marshal.AllocHGlobal( InjectedScript.Length );
            Marshal.Copy( InjectedScript , 0 , memIntPtr , InjectedScript.Length );


            IntPtr allocMemAddress = VirtualAllocEx( processHandle , IntPtr.Zero , (uint)InjectedScript.Length , MEM_COMMIT | MEM_RESERVE , PAGE_READWRITE );
            WriteProcessMemory( (int)processHandle , (long)allocMemAddress , (byte**)memIntPtr , InjectedScript.Length , out int bytesRead );

            byte* bruh = (byte*)allocMemAddress; //we are writing a pointer not the pointer's value so need to do this

            long dataaddressaa = (long)allocMemAddress + vars[0] + InjectedScript.Length;

            long dataaddress = ((long)processBAddr + 0xD9CDE6E6 ) ;

            WriteProcessMemory( (int)processHandle , dataaddress , &bruh , sizeof( long ) , out bytesRead );
            //WriteProcessMemory( (int)processHandle , headerPointer + 0x10 , &bruh , sizeof( long ) , out bytesRead );


            AddLog( "Script injected successfully!" );

            AddLog( "" );
            AddLog( "------------------------------" );
        }



        public unsafe void InjectScriptV1( string GSCPath )
        {
            if ( !UpdateProcessInfo( ) )
            {
                AddLog( "MW2019 is not running!" );
                return;
            }

            long AssetPoolOffset = ( (long)processBAddr + ( 0x7FF6BD4CaB80 - 0x7FF6B4000000 ) ) - 0x1000;

            var AssetPools = ReadArrayUnsafe<XAssetEntry>( AssetPoolOffset , 156672 );
            long headerPointer = 0;
            foreach ( var Asset in AssetPools )
            {
                if ( Asset.type == 0x36 && (long)Asset.HeaderPointer != 0 ) //make sure asset is scriptparsetree type (0x36) and is not empty
                {
                    if ( ReadNullTerminatedString( BitConverter.ToInt64( ReadBytes( (long)Asset.HeaderPointer , 8 ) , 0 ) ).Equals( "scripts/shared/duplicaterender_mgr.gsc" ) )
                    {
                        headerPointer = (long)Asset.HeaderPointer; //pointer to the header
                        break;

                    }
                }
            }
            if ( headerPointer == 0 )
            {
                AddLog( "Unable to find Header to Inject! Make sure you are in the main menu." );
                return;
            }

            long assetbufferB = BitConverter.ToInt64( ReadBytes( headerPointer + 0x10 , 8 ) , 0 );

            byte[] InjectedScript = File.ReadAllBytes( GSCPath );
            if ( InjectedScript[4] != 0x0D && InjectedScript[5] != 0x0A )
            {
                AddLog( "This is not a compiled GSC file! Double check that you aren't trying to inject source code." );
                return;
            }

            memIntPtr = Marshal.AllocHGlobal( InjectedScript.Length );
            Marshal.Copy( InjectedScript , 0 , memIntPtr , InjectedScript.Length );



            IntPtr allocMemAddress = VirtualAllocEx( processHandle , IntPtr.Zero , (uint)InjectedScript.Length , MEM_COMMIT | MEM_RESERVE , PAGE_READWRITE );
            WriteProcessMemory( (int)processHandle , (long)allocMemAddress , (byte**)memIntPtr , InjectedScript.Length , out int bytesRead );

            byte* bruh = (byte*)allocMemAddress; //we are writing a pointer not the pointer's value so need to do this

            WriteProcessMemory( (int)processHandle , headerPointer + 0x10 , &bruh , sizeof( long ) , out bytesRead );

            long assetbufferA = BitConverter.ToInt64( ReadBytes( headerPointer + 0x10 , 8 ) , 0 );

            if ( assetbufferA != assetbufferB )
                AddLog( "Script injected successfully!" );
            else
                AddLog( "Unable to write to MW2019's memory!" );

            AddLog( "" );
            AddLog( "------------------------------" );
        }






        public MW2019GscInjector( )
        {
            InitializeComponent( );
        }

        private void buttonInjectGSC_Click( Object sender , EventArgs e )
        {

            var file = new OpenFileDialog { Filter = "Compiled GSC File|*." + textboxExtName.Text };
            if ( file.ShowDialog( ) != DialogResult.OK ) return;
            using ( file )
            {
                InjectScriptV2( file.FileName );
            }
        }

        private void MW2019GscInjector_Load( Object sender , EventArgs e )
        {

        }

        private void buttonClearList_Click( Object sender , EventArgs e )
        {
            richtextboxLog.Clear( );
        }



        private void AddLog(string text)
        {
            richtextboxLog.AppendText("[" + DateTime.Now.ToString() + "]\n");
            richtextboxLog.AppendText( text + "\n" );
        }
    }
}
