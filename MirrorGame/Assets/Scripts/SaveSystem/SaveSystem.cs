using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

/*
* Used the following Youtube video for code, added addition error handling but the core of the code is the same
* Brackeys (2018). SAVE & LOAD SYSTEM in Unity. YouTube. Available at: https://www.youtube.com/watch?v=XOjd_qU2Ido [Accessed 28 April 2022].
*/
public static class SaveSystem
{
    public static void Save(PlayerAbilities playerAbilities, PlayerMirrored playerMirrored)
    {
        FileStream stream = null;
        BinaryFormatter formatter = new BinaryFormatter();
        string path = GetPath();
        try{
            stream = new FileStream(path, FileMode.Create); 

            PlayerData data = new PlayerData(playerAbilities, playerMirrored);

            formatter.Serialize(stream, data);

            if (stream != null)
            {
                stream.Close();
            }
        }
        catch(IOException){
            if (stream != null)
            {
                stream.Close();
            }
            return;
        }
    }

    public static PlayerData Load()
    {
        FileStream stream = null;
        string path = GetPath();

        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            try{
                stream = new FileStream(path, FileMode.Open);

                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                if (stream != null)
                {
                    stream.Close();
                }
                return data;
            }
            catch(IOException){
                if (stream != null)
                {
                    stream.Close();
                }
                return null;
            }
        }else{
            Debug.Log("Save file not found in " + path);
            return null;
        }
    }

    private static string GetPath()
    {
        return Application.persistentDataPath + "/player.dat";
    }
}
