using System;
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
                Transform playerTransform = GetPlayerTransform();
                byte[] buffer = SerializeVector(playerTransform.position);
                 stream.Write(buffer, 0, buffer.Length);
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
                Transform playerTransform = GetPlayerTransform();
                playerTransform.position = DeserializeVector(buffer);
            }
        }

        Transform GetPlayerTransform()
        {
            return GameObject.FindWithTag("Player").transform;
        }

        byte[] SerializeVector(Vector3 vector)
        {
            //3 bytes at a length of 4
            byte[] vectorBytes = new byte[3 * 4];
            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);
            return vectorBytes;
        }

        Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 result = new Vector3();
            //ToSingle is basically the same as ToFloat
            result.x = BitConverter.ToSingle(buffer, 0);
            result.y = BitConverter.ToSingle(buffer, 4);
            result.z = BitConverter.ToSingle(buffer, 8);
            return result;

        }

        string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, saveFile + ".sav");
        }
    }
}
