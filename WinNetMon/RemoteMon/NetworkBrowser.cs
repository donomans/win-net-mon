using System;
using System.Collections;
using System.DirectoryServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace RemoteMon
{
    public static class NetworkBrowser
    {
        #region Public Methods

        public static ArrayList GetNetworkComputers()
        {
            //TODO: need to convert this to a dict possibly?  currently there can be duplicates
            ArrayList ar = new ArrayList();
            ar.AddRange(GetNetworkWin32Api());
            ar.AddRange(GetNetworkDirectoryServices());
            return ar;
        }

        private static ArrayList GetNetworkDirectoryServices()
        {
            DirectoryEntry de = new DirectoryEntry("WinNT:");
            ArrayList ar = new ArrayList();
            foreach (DirectoryEntry nDom in de.Children)
            {
                foreach (DirectoryEntry nPc in nDom.Children)
                {
                    if (nPc.Name != "Schema")
                        ar.Add(nPc.Name);
                }
            }
            de.Dispose();
            return ar;
        }

        private static ArrayList GetNetworkWin32Api()
        {
            ArrayList networkComputers = new ArrayList();
            const int MAX_PREFERRED_LENGTH = -1;

            /*
                const uint SV_TYPE_WORKSTATION = 0x1
                const uint SV_TYPE_SERVER = 0x2
                const uint SV_TYPE_SQLSERVER = 0x4
                const uint SV_TYPE_DOMAIN_CTRL = 0x8
                const uint SV_TYPE_DOMAIN_BAKCTRL = 0x10
                const uint SV_TYPE_TIME_SOURCE = 0x20
                const uint SV_TYPE_AFP = 0x40
                const uint SV_TYPE_NOVELL  = 0x80
                const uint SV_TYPE_DOMAIN_MEMBER = 0x100
                const uint SV_TYPE_PRINTQ_SERVER = 0x200
                const uint SV_TYPE_DIALIN_SERVER = 0x400
                const uint SV_TYPE_XENIX_SERVER = 0x800
                const uint SV_TYPE_SERVER_UNIX = SV_TYPE_XENIX_SERVER
                const uint SV_TYPE_NT = 0x1000
                const uint SV_TYPE_WFW = 0x2000
                const uint SV_TYPE_SERVER_MFPN = 0x4000
                const uint SV_TYPE_SERVER_NT = 0x8000&
                const uint SV_TYPE_POTENTIAL_BROWSER = 0x10000
                const uint SV_TYPE_BACKUP_BROWSER = 0x20000
                const uint SV_TYPE_MASTER_BROWSER = 0x40000
                const uint SV_TYPE_DOMAIN_MASTER = 0x80000
                const uint SV_TYPE_SERVER_OSF = 0x100000
                const uint SV_TYPE_SERVER_VMS = 0x200000
                const uint SV_TYPE_WINDOWS = 0x400000 //Windows95 and above
                const uint SV_TYPE_DFS = 0x800000 //Root of a DFS tree
                const uint SV_TYPE_CLUSTER_NT = 0x1000000 //NT Cluster
                const uint SV_TYPE_TERMINALSERVER = 0x2000000 //Terminal Server
                const uint SV_TYPE_DCE = 0x10000000 //IBM DSS (Directory and Security Services) or equivalent
                const uint SV_TYPE_ALTERNATE_XPORT = 0x20000000 //Return list for alternate transport
                const uint SV_TYPE_LOCAL_LIST_ONLY = 0x40000000 //Return local list only
                const uint SV_TYPE_DOMAIN_ENUM = 0x80000000 //Return domain list only
                const uint SV_TYPE_ALL = 0xFFFFFFFF
            */
            //const uint SV_TYPE_WORKSTATION = 1;
            //const uint SV_TYPE_SERVER = 2;
            //const uint SV_TYPE_WINDOWS = 8290304;
            const uint SV_TYPE_ALL = 0xFFFFFFFF; //4294967295;
            IntPtr buffer = IntPtr.Zero;

            int sizeofInfo = Marshal.SizeOf(typeof (NativeMethods._SERVER_INFO_100));


            try
            {
                int resHandle = 0;
                int totalEntries = 0;
                int entriesRead = 0;
                int ret = NativeMethods.NetServerEnum(null, 100, ref buffer,
                                        MAX_PREFERRED_LENGTH,
                                        out entriesRead,
                                        out totalEntries,
                                        SV_TYPE_ALL, //SV_TYPE_WORKSTATION | SV_TYPE_SERVER | SV_TYPE_WINDOWS, 
                                        null, 
                                        out resHandle);
                if (ret == 0)
                {
                    for (int i = 0; i < totalEntries; i++)
                    {
                        IntPtr tmpBuffer = new IntPtr((int) buffer +
                                                      (i*sizeofInfo));

                        NativeMethods._SERVER_INFO_100 svrInfo = (NativeMethods._SERVER_INFO_100)
                                                   Marshal.PtrToStructure(tmpBuffer,
                                                                          typeof(NativeMethods._SERVER_INFO_100));
                        networkComputers.Add(svrInfo.sv100_name);
                    }
                }
            }
            catch (ThreadAbortException) { }
            catch (Exception ex)
            {
                MessageBox.Show("Problem with acessing " +
                                "network computers in NetworkBrowser " +
                                "\r\n\r\n\r\n" + ex.Message,
                                "Error", MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                NativeMethods.NetApiBufferFree(buffer);
            }
            return networkComputers;
        }

        #endregion
    }
}