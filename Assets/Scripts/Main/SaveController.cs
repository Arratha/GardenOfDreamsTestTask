using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

using UnityEngine;

using Player;

namespace Main
{
    public static class SaveController
    {
        private const string SaveFile = "/save.bin";

        public static void Save(List<PlayerInventory.InventoryCell> saveData)
        {
            string path = Application.persistentDataPath + SaveFile;

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            formatter.Serialize(file, saveData);

            file.Close();
        }

        public static List<PlayerInventory.InventoryCell> Load()
        {
            List<PlayerInventory.InventoryCell> result = new List<PlayerInventory.InventoryCell>();

            string path = Application.persistentDataPath + SaveFile;

            if (File.Exists(path))
            {
                FileStream file = File.Open(path, FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    result = (List<PlayerInventory.InventoryCell>)formatter.Deserialize(file);
                    file.Close();
                }
                catch
                {
                    file.Close();
                    File.Delete(path);
                }
            }

            return result;
        }
    }
}