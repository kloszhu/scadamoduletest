using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ys.AutoSwitch.Proto;

namespace Ys.AutoSwitch.socket.Core
{
    public class ServerDataManager : IDisposable
    {
        public ConcurrentQueue<SwitchMsg> Msg;
        public CancellationToken CancellationToken;
        public ActionHandler _ActionManager;
        public ServerDataManager(ActionHandler ActionManager)
        {
            _ActionManager = ActionManager;
            Msg = new ConcurrentQueue<SwitchMsg>();
            Deal();
        }

        public Task Deal()
        {
            return Task.Run(() =>
            {
                while (!CancellationToken.IsCancellationRequested)
                {
                    while (Msg.TryDequeue(out SwitchMsg msg))
                    {

                        switch (msg.MsgType)
                        {
                            case MsgType.PingType:
                                _ActionManager.Ping(msg.Pingpack);
                                break;
                            case MsgType.PongType:
                                _ActionManager.Pong(msg.Pongpack);
                                break;
                            case MsgType.PrepareBackupType:
                                _ActionManager.PrepareBackup(msg.PrepareBackup);
                                break;
                            case MsgType.MasterDownType:
                                _ActionManager.MasterDown();
                                break;
                            case MsgType.MasterOkType:
                                _ActionManager.MasterOk();
                                break;
                            case MsgType.BackupOnType:
                                _ActionManager.BackupOn();
                                break;
                            case MsgType.SwitchMasterType:
                                _ActionManager.SwitchMaster();
                                break;
                            case MsgType.SwitchBackupType:
                                _ActionManager.SwitchBackup();
                                break;
                            case MsgType.TongBuType:
                                _ActionManager.TongBu(msg.TongBu);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }, CancellationToken);

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            GC.KeepAlive(this);
            GC.Collect();
        }
    }
}
