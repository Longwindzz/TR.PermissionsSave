using Rocket.Core;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TR.权限组自动保存
{
    public class Plugin:RocketPlugin
    {
        public static Plugin Instance;

        public string path2 = System.Environment.CurrentDirectory;
        public FileSystemWatcher watcher = new FileSystemWatcher();
        protected override void Load()
        {
            Instance = this;
            Rocket.Core.Logging.Logger.Log("Welcome to use TR.PermissionsSave！——By TR.Plugin");
            Rocket.Core.Logging.Logger.Log("———————————");
            Rocket.Core.Logging.Logger.Log("————Author—————");
            Rocket.Core.Logging.Logger.Log("————长风—————");
            Rocket.Core.Logging.Logger.Log("——TCD.ROCKETMOD———");
            Rocket.Core.Logging.Logger.Log("Verson 1.1.0 Load Succused！");
            Rocket.Core.Logging.Logger.Log("Get the directory......");
            Rocket.Core.Logging.Logger.Log("Got it：" + path2+"!");
            watcher.Filter = "Permissions.config.xml";
            watcher.Path = @path2;
            watcher.Changed += new FileSystemEventHandler(文件更新);
            watcher.EnableRaisingEvents = true;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
        }

        public void 文件更新(object sender, FileSystemEventArgs e)
        {
            Rocket.Core.Logging.Logger.Log("The Permissions is reload!");
            权限更新();
        }

        public void 权限更新()
        {
            watcher.Changed -= new FileSystemEventHandler(文件更新);
            R.Permissions.Reload();
            StartBroadcast();
        }
        public void StartBroadcast()
        {
            StartCoroutine(ClearEffectCoroutine(1));
        }

        public IEnumerator ClearEffectCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            watcher.Changed += new FileSystemEventHandler(文件更新);
        }

        protected override void Unload()
        {
            watcher.Changed -= new FileSystemEventHandler(文件更新);
        }
    }
}
