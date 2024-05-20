using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.AutoSwitch.Proto;

namespace Ys.AutoSwitch.socket.Core
{
    public class ClientMessageHandler : ActionHandler
    {
        public override Action<PingPackMSG> Callback1 { get; set; }

        public override Action Callback { get; set; }

        public override void BackupOn()
        {
            Console.WriteLine("");
        }



        public override void MasterDown()
        {
            Console.WriteLine("");
        }

        public override void MasterOk()
        {
            Console.WriteLine("");
        }

        public override void Ping(PingPackMSG pingpack)
        {
            Console.WriteLine("");
        }

        public override void Pong(PongPackMSG pongpack)
        {
            Console.WriteLine("");
        }

        public override void PrepareBackup(PrepareBackupMSG prepareBackup)
        {
            Console.WriteLine("");
        }

        public override void SwitchBackup()
        {
            Console.WriteLine("");
        }

        public override void SwitchMaster()
        {
            Console.WriteLine("");
        }

        public override void TongBu(TongBu tongBu)
        {
            Console.WriteLine("");
        }
    }
}
