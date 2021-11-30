using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {

        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Saving to" + path);
            //Creates new file and overwrites old file if one exists
            //Adding using prevents possible errors causing file to not close and instead, leak
            using (FileStream stream = File.Open(path, FileMode.Create))
            {
                 //Example of writing to saveFile using an array of bytes
                 byte[] bytes = Encoding.UTF8.GetBytes("Testing Save Text");
                 stream.Write(bytes, 0, bytes.Length);
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            print("Loading from" + path);
            //FileMode.Open doesn't overwrite any existing file
            using (FileStream stream = File.Open(path, FileMode.Open))
            {
                //A buffer is a piece of allocated memory that we created for something else to fill in
                //Typically, buffer array should be a fixed, constant size and it would iterate over file multiple times in chunks.
                byte[] buffer = new byte[stream.Length];
                //Read returns an int of how many bytes it read
                stream.Read(buffer, 0, buffer.Length);
                //Converting bytes in buffer into a string
               print(Encoding.UTF8.GetString(buffer));
            }
        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
