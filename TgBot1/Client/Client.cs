using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MusicaBot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MusicaBot.Client
{
    public class Client
    {
        private HttpClient _httpClient;           
        private static string _adress;
       

        public Client()
        {
           

            _adress = Constants.addres;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_adress);
        }


        public async Task<string> FindSong(string Name, string user)
        {
            var responce = await _httpClient.GetAsync($"/Music/Music/Author?Name={Name}&Type=Tracks&Limit=1&user={user}");

            //if (responce.IsSuccessStatusCode)
            //{
                var answer = responce.Content.ReadAsStringAsync().Result;
                var name = JsonConvert.DeserializeObject<Models.ResponceModel>(answer);

                var s = " ";

                if (name != null)
                {
                    s = $"Link on Sporify: https://open.spotify.com/track/{name.uri.Remove(0, 14)} \nAuthor's/Grop's name: {name.Profname}\nSpotify profile link: https://open.spotify.com/artist/{name.profurl.Remove(0, 14)}\nSong full name: {name.name} \nAlbum name: {name.Albname}\nAlbum link: https://open.spotify.com/album/{name.alburi.Remove(0, 14)}\nTo find lyrics use /findlyrics\n Add song to list /save";
                    ID.id = name.id;
                }
                else { s = "Nice try. Error in input"; }


              

                

                return s;
            //}
            //else return "Error, no data";
             
        }



        public async Task<string> FindTop5(string Artist)
        {
            var responce = await _httpClient.GetAsync($"/Music/Top10?Artist={Artist}");

            if (responce.IsSuccessStatusCode)
            {
                var answer = responce.Content.ReadAsStringAsync().Result;
                var name = JsonConvert.DeserializeObject<Models.Tmodel>(answer);

                var s = " ";

                if (name.track != null)
                {
                    //a = $"Artist's image: {name.track[1].strMusicVid.Length}\n";

                    for (int i = 0; i < 5; i++)
                    {
                        s += $"Track: {name.track[i].strTrack} \nAlbum: {name.track[i].strAlbum}\nArtist: {name.track[i].strArtist}\nGenre: {name.track[1].strGenre}\nTotal plays: {name.track[i].intTotalPlays}\nLink on YouTube: {name.track[i].strMusicVid}\n\n";
                    }


                }
                else return ("Incorrect input, or there are no data about this artist. Please, try again");
                return s;
            }
            else return "Error, no data";

        }

        public async Task<string> FindBio(string author)
        {
            var responce = await _httpClient.GetAsync($"/Music/Bio?Author={author}");

            if (responce.IsSuccessStatusCode)
            {
                var answer = responce.Content.ReadAsStringAsync().Result;
                var res = JsonConvert.DeserializeObject<Models.Amodel>(answer);

                var s = " ";


                if (res.artists != null)
                {
                    for (int i = 0; i < res.artists.Length; i++)
                    {
                        s += $"Name: {res.artists[i].strArtist}\nDate of birth: {res.artists[i].intBornYear}\nBiography: {res.artists[i].strBiographyEN}\nImage: {res.artists[i].strArtistBanner}\nFaceBook: {res.artists[i].strFacebook}\n";
                    }
                }
                else return ("Sorry, try again or there is no info about this artist.  Please, try again");



                return s;
            }
            else return "Error, no data";

        }
        
        string id = ID.id;



        public async Task<string> GetLyrics()
        {
            var responce = await _httpClient.GetAsync($"/Music/Lyrics");

            if (responce.IsSuccessStatusCode)
            {
                var answer = responce.Content.ReadAsStringAsync().Result;
                var ans = JsonConvert.DeserializeObject<Models.Line>(answer);

                var s = " ";

                
                if (ans.words != null)
                {
                    
                        s += $"Lyrics: {ans.words}\n\nTo find translation of the lyrics use  /findtranslate\n";

                    TEST.word.Add(ans.words + ".");
                }
                else return ("Sorry, try again ");


                return s;
            }
            else return "Error, no data";

        }


        public async Task<string> GetTranslate()
        {
            string beb = " ";

           

            var data = new Dictionary<string, string>
            {
                {"to","uk"},
                {"text",$"{beb}"}
            };



            var responce = await _httpClient.PostAsync($"/Music/Translate", new FormUrlEncodedContent(data));   



            if (responce.IsSuccessStatusCode)
            {
                var answer = await responce.Content.ReadAsStringAsync();
                var t = JsonConvert.DeserializeObject<Models.TRANSLATEmodel>(answer);

                string s = null;



                if (t.translated_text != null)
                {

                    s = $"Translation: {t.translated_text}\n";

                }
                else return ("Error in search");
                if (t.translated_text == "")
                {
                    s = "Error";
                }

                return s;

            }
            else return "Error, no data";


        }


        public async Task AddtFav(string name)
        {
            var responce = await _httpClient.GetAsync($"/Music/addtofav?user={name}");

           
                var answer = responce.Content.ReadAsStringAsync().Result;
                
        }


        public async Task<string> GetList(string name)
        {
            var responce = await _httpClient.GetAsync($"/Music/favourites?user={name}");


            var answer = responce.Content.ReadAsStringAsync().Result;
            var ans = JsonConvert.DeserializeObject<List<ResponceModel>>(answer);

            string s = " ";

            if (ans.Count > 0)
            {
                for (int i = 0; i < ans.Count; i++)
                {
                    s += $"{i + 1}. Link on Sporify: https://open.spotify.com/track/{ans[i].uri.Remove(0, 14)}\nAuthor's/Grop's name: {ans[i].Profname}\nSpotify profile link: Spotify profile link: https://open.spotify.com/artist/{ans[i].profurl.Remove(0, 14)}\nSong full name: {ans[i].name}\nAlbum name: {ans[i].Albname}\nAlbum link: {ans[i].alburi}\n\n";
                }
            }
            else { s = "Empty list"; }
            
            
            return s;
        }



        public async Task<string> Delete(string name, int number)
        {


            var responce = await _httpClient.DeleteAsync($"/Music/delete?user={name}&numb={number}");

            return responce.Content.ReadAsStringAsync().Result;
        }
    }

}






