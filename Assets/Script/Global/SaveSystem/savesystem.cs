using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSystem 
{
   // option
   public static void saveoption(savedata player)
   {
      BinaryFormatter formatter = new BinaryFormatter();
      string path = Application.persistentDataPath + "/saveFile";
      FileStream stream = new FileStream(path, FileMode.Create);

      saveproggres data = new saveproggres(player);


      formatter.Serialize(stream, data);
      stream.Close();
   }
   public static saveproggres Loadoption()
   {
     string path = Application.persistentDataPath + "/saveFile";
     if(File.Exists(path))
     {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Open);

       saveproggres data = formatter.Deserialize(stream) as saveproggres ;
       stream.Close();

       return data;
     }
     else
     {
      Debug.LogError("SaveFileNotFound" + path);
      return null;
     }
   }

}
