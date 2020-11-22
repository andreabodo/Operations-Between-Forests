using GraphX.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Win32;
using OperationsBetweenForests.Core;

namespace OperationsBetweenForests.Serialization
{
    public static class FileManager
    {
        #region Serialization/deserialization to/from json
        /// <summary>
        /// Prende in input un oggetto e ne restituisce la stringa in formato JSON
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static void SerializeToJsonFile(Object o, String filename)
        {
            File.WriteAllText(filename, JsonSerializer.Serialize(o));
        }

        /// <summary>
        /// Salva l'oggetto in json nella posizione selezionata
        /// </summary>
        public static void SaveToJsonFile(Object o)
        {
            SaveFileDialog dialog = new SaveFileDialog() {Title="Scegli dove salvare il file", Filter ="TreeFile | *.JSON" };
            if (dialog.ShowDialog() == true)
            {
                SerializeToJsonFile(o, dialog.FileName);
            }
        }

        /// <summary>
        /// Carica l'oggetto Node dal file json selezionato
        /// </summary>
        /// <returns></returns>
        public static object DeserializeFromJsonFile()
        {
            OpenFileDialog dialog = new OpenFileDialog { Title = "Scegli il file che vuoi aprire", Filter = "TreeFile | *.JSON" };
            if (dialog.ShowDialog() == true)
            {
                return JsonSerializer.Deserialize<Forest>(File.ReadAllText(dialog.FileName));
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region DOTFile Management
        public static void SaveDotFile(String fileName, String DOTContent)
        {
            /*
            if(File.Exists(@"DOTGraphs/" + fileName + ".dot"))
            {

            }*/
            File.WriteAllText(@"DOTGraphs/" + fileName + ".dot", DOTContent);
        }
        #endregion


    }
}
