﻿using GraphX.Common.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAXLib;
using System.Text.Json;
using Microsoft.Win32;
using OperationsBetweenForests.Models;
using OperationsBetweenForests.Core;

namespace OperationsBetweenForests.Serialization
{
    public static class FileManager
    {
        #region Old method
        [Obsolete]
        /// <summary>
        /// Serializes data classes list to file
        /// </summary>
        /// <param name="filename">File name</param>
        /// <param name="modelsList">Data classes list</param>
        public static void SerializeDataToFile(string filename, List<GraphSerializationData> modelsList)
        {
            using (FileStream stream = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                SerializeDataToStream(stream, modelsList);
            }
        }
        [Obsolete]
        /// <summary>
        /// Deserializes data classes list from file
        /// </summary>
        /// <param name="filename">File name</param>
		public static List<GraphSerializationData> DeserializeDataFromFile(string filename)
        {
            using (FileStream stream = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return DeserializeDataFromStream(stream);
            }
        }
        [Obsolete]
        /// <summary>
        /// Serializes graph data list to a stream
        /// </summary>
        /// <param name="stream">The destination stream</param>
        /// <param name="modelsList">The graph data</param>
		public static void SerializeDataToStream(Stream stream, List<GraphSerializationData> modelsList)
        {
            var serializer = new YAXSerializer(typeof(List<GraphSerializationData>));
            using (var textWriter = new StreamWriter(stream))
            {
                serializer.Serialize(modelsList, textWriter);
                textWriter.Flush();
            }
        }
        [Obsolete]
        /// <summary>
        /// Deserializes graph data from a stream
        /// </summary>
        /// <param name="stream">The stream</param>
        /// <returns>The graph data</returns>
		public static List<GraphSerializationData> DeserializeDataFromStream(Stream stream)
        {
            var deserializer = new YAXSerializer(typeof(List<GraphSerializationData>));
            using (var textReader = new StreamReader(stream))
            {
                return (List<GraphSerializationData>)deserializer.Deserialize(textReader);
            }
        }
        #endregion

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
