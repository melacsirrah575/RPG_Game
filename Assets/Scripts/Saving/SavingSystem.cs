using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {

        public void Save(string saveFile)
        {
            Dictionary<string, object> state = LoadFile(saveFile);
            CaptureState(state);
            SaveFile(saveFile, state);
        }

        public void Load(string saveFile)
        {
            RestoreState(LoadFile(saveFile));
        }

        private Dictionary<string, object> LoadFile(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            if(File.Exists(path))
            {
                return new Dictionary<string, object>();
            }
            //FileMode.Open doesn't overwrite any existing file
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (Dictionary<string, object>)formatter.Deserialize(stream);
            }
        }

        private void SaveFile(string saveFile, object state)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to" + path);
            //Creates new file and overwrites old file if one exists
            //Adding using prevents possible errors causing file to not close and instead, leak
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                //Have to create method before able to call methods from it
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach(SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach (SaveableEntity saveable in FindObjectsOfType<SaveableEntity>())
            {
               string id = saveable.GetUniqueIdentifier();
               if(state.ContainsKey(id))
               {
                    saveable.RestoreState(state[id]);
               }

            }
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
