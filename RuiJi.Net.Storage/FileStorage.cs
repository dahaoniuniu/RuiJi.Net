﻿using RuiJi.Net.Storage.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RuiJi.Net.Storage
{
    public class FileStorage : StorageBase<DownloadContentModel>
    {
        static FileStorage()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "download");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public FileStorage(string folder, string databaseName = "", string collectionName = "") : base(folder, databaseName, collectionName)
        {

        }

        public override int Insert(DownloadContentModel content)
        {
            try
            {
                var ext = Path.GetExtension(content.Url).ToLower();
                var name = GetMD5Hash(content.Url);
                if(string.IsNullOrEmpty(ext))
                {
                    ext = ".txt";
                }

                var path = Path.Combine(ConnectString, name + ext);

                if (content.IsRaw)
                {                    
                    var raw = Convert.FromBase64String(content.Data.ToString());

                    File.WriteAllBytes(path, raw);
                }
                else
                    File.WriteAllText(path, content.Data.ToString());
            }
            catch { }

            return 0;
        }

        public override int Insert(DownloadContentModel[] contents)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        public override bool Remove(string url)
        {
            throw new NotImplementedException();
        }

        public override bool Update(DownloadContentModel content)
        {
            throw new NotImplementedException();
        }

        public static string GetMD5Hash(String input)
        {
            MD5 md5Hasher = new MD5CryptoServiceProvider();
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}