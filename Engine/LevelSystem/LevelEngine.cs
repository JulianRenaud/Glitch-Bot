using DSharpPlus.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Glitch_Bot.Engine.LevelSystem
{
    public class LevelEngine
    {
        public bool levelledUp = false;
        public bool StoreDUserDetails(DUser user)
        {
            try
            {
                var path = @"C:\Users\Pearl-Laptop\Desktop\Discord-Bot\Glitch-Bot\bin\Debug\DUserInfo.json";

                var json = File.ReadAllText(path);
                var jsonObj = JObject.Parse(json);

                var members = jsonObj["members"].ToObject<List<DUser>>();
                members.Add(user);

                jsonObj["members"] = JArray.FromObject(members);
                File.WriteAllText(path, jsonObj.ToString());

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckUserExists(string username, ulong guildID)
        {
            using (StreamReader streamReader = new StreamReader("DUserInfo.json"))
            {
                string json = streamReader.ReadToEnd();
                LevelJSONFile userToGet = JsonConvert.DeserializeObject<LevelJSONFile>(json);

                foreach (var user in userToGet.members)
                {
                    if (user.UserName == username && user.GuildID == guildID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public DUser GetUser(string username, ulong guildID)
        {
            using (StreamReader streamReader = new StreamReader("DUserInfo.json"))
            {
                string json = streamReader.ReadToEnd();
                LevelJSONFile userToGet = JsonConvert.DeserializeObject<LevelJSONFile>(json);

                foreach (var user in userToGet.members)
                {
                    if (user.UserName == username && user.GuildID == guildID)
                    {
                        return user;
                    }
                }
            }
                return null;
        }

        public bool AddXP(string username, ulong guildID)
        {
            levelledUp = false;

            try
            {
                var path = @"C:\Users\Pearl-Laptop\Desktop\Discord-Bot\Glitch-Bot\bin\Debug\DUserInfo.json";

                var json = File.ReadAllText(path);
                var jsonObj = JObject.Parse(json);
                

                var members = jsonObj["members"].ToObject<List<DUser>>();


                foreach (var user in members)
                {

                    if (user.UserName == username && user.GuildID == guildID)
                    {
                        var countXP = new Random().Next(45, 75);
                        user.XP = user.XP + countXP;
                        var finalXP = (3 * (user.Level * 10 + 195));
                        if (user.XP >= finalXP)
                        {
                            levelledUp = true;
                            user.Level++;
                            user.XP = user.XP - finalXP;
                        }
                        user.MaxXP = (3 * (user.Level * 10 + 195));
                    }
                    
                }

                jsonObj["members"] = JArray.FromObject(members);
                File.WriteAllText(path, jsonObj.ToString());

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool GetLatestTime(string username, ulong guildID)
        {
            try
            {
                var path = @"C:\Users\Pearl-Laptop\Desktop\Discord-Bot\Glitch-Bot\bin\Debug\DUserInfo.json";

                var json = File.ReadAllText(path);
                var jsonObj = JObject.Parse(json);


                var members = jsonObj["members"].ToObject<List<DUser>>();
                foreach (var user in members)
                {
                    if (user.UserName == username && user.GuildID == guildID)
                    {
                        var Time = DateTime.Now.TimeOfDay;
                        if (user.Timespan.Hours == Time.Hours && user.Timespan.Days == Time.Days)
                        {
                            if (user.Timespan.Minutes < Time.Minutes)
                            {
                                user.Timespan = Time;
                                jsonObj["members"] = JArray.FromObject(members);
                                File.WriteAllText(path, jsonObj.ToString());
                                return true;
                            }
                        }
                        else
                        {
                            user.Timespan = Time;
                            jsonObj["members"] = JArray.FromObject(members);
                            File.WriteAllText(path, jsonObj.ToString());
                            return true;
                        }
                    }
                }
                return false; 
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

    class LevelJSONFile
    {
        public string userInfo { get; set; }
        public DUser[] members { get; set; }
    }
}
