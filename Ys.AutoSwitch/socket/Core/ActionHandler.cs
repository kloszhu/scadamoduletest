using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.AutoSwitch.Proto;

namespace Ys.AutoSwitch.socket
{

    public abstract class ActionHandler
    {
        public abstract Action<PingPackMSG> Callback1 { get; set; }
        public abstract Action Callback { get; set; }
        public abstract void Ping(PingPackMSG pingpack);
        public abstract void BackupOn();
        public abstract void MasterDown();
        public abstract void MasterOk();
        public abstract void Pong(PongPackMSG pongpack);
        public abstract void PrepareBackup(PrepareBackupMSG prepareBackup);
        public abstract void SwitchBackup();
        public abstract void SwitchMaster();
        public abstract void TongBu(TongBu tongBu);
    }
}
