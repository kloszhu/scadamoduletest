using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.AutoSwitch.Proto;

namespace Ys.AutoSwitch.socket.Core
{
    public class ServerMessageHandler : ActionHandler
    {
        public override Action Callback { get; set; }
        public override Action<PingPackMSG> Callback1 { get; set; }

        public override void BackupOn()
        {
            Console.WriteLine("111");
        }
        public override void MasterDown()
        {
            Console.WriteLine("111");
        }

        public override void MasterOk()
        {
            Console.WriteLine("111");
        }

        public override void Ping(PingPackMSG pingpack)
        {
            Console.WriteLine("111");
        }

        public override void Pong(PongPackMSG pongpack)
        {
            Console.WriteLine("111");
        }

        public override void PrepareBackup(PrepareBackupMSG prepareBackup)
        {
            Console.WriteLine("111");
        }

        public override void SwitchBackup()
        {
            Console.WriteLine("111");
        }

        public override void SwitchMaster()
        {
            Console.WriteLine("111");
        }

        public override void TongBu(TongBu tongBu)
        {
            Console.WriteLine("111");
        }
    }
}
