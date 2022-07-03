using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace MusicaBot
{
    public class Program
    {
        

        static ITelegramBotClient bot = new TelegramBotClient("5464216389:AAFH7R8qQBw49l06imwyU2UPgitQbvB0cUo");
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            var _client = new Client.Client();
            //Вывод на консоль сообщения от пользователя
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            try
            {


                if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
                {


                    try
                    {



                        var message = update.Message;

                        if (message.Text != null)
                        {



                            if (message.Text.ToLower() == "/start")
                            {

                                await botClient.SendTextMessageAsync(message.Chat, "Welcome to the 🎵MusicaBot🎵\nThis bot will help you find your song, lyrics and translation into Ukrainian only!\nAlso, it might help you find the top 5 songs of some top-rated artists and their biography.\nYou can save the song you find to your special 🎵MusicaList🎵\nFor a list of commands, use /help");

                            }


                        
                        

                          if (message.Text.ToLower() == "/help" )
                          {
                            await botClient.SendTextMessageAsync(message.Chat, "‼️Main functionality‼️\n\n/findsong - Search for specific song on spotify, if you want to find the exact song be sure to add the group's or artist's name\nExample: (/findsong The Score Revolution) \n\n " +
                                "/findtop5 - Search for top 5 songs of specific author, this method is not connected to Spotify, so there is possibility that you might not find your's artist's stats\nExampe: (/findtop5 Ariana Grande)\n\n " +
                                "/findbio - Find a biography of a specific author, this method is not connected to Spotify, so there is a possibility that you might not find your's artist's bio\nExample:(/findbio Lady Gaga)\n" +
                                "\n🎵MusicaList🎵\n\n" +
                                "/list - If you want to check what you already have added to your list, use this command\n\n" +
                                "/delete - If you want to delete a song from your list you can use this command, do not forget to write a number of song you want to delete\nExample: (/delete 1)");

                          }

                         else if (message.Text.ToLower().Contains("/findsong" + " "))
                         {

                            var song = message.Text.Replace("/findsong ", "");
                            try
                            {
                                var str = await _client.FindSong(song, message.Chat.Id.ToString());
                                await botClient.SendTextMessageAsync(message.Chat, str);
                            }

                            catch { await botClient.SendTextMessageAsync(message.Chat, "Error in search or incorrect input. Please, try again. Hope you didn't broke anything"); }


                         }

                          else if (message.Text.ToLower().Contains("/findtop5" + " "))
                          {
                            var top = message.Text.Replace("/findtop5 ", "");
                            try
                            {
                                var str = await _client.FindTop5(top);
                                await botClient.SendTextMessageAsync(message.Chat, str);
                            }

                            catch { await botClient.SendTextMessageAsync(message.Chat, "Error in search"); }

                          }

                            else if (message.Text.ToLower().Contains("/findbio" + " "))
                            {

                                var author = message.Text.Replace("/findbio ", "");
                                try
                                {
                                    var str = await _client.FindBio(author);
                                    await botClient.SendTextMessageAsync(message.Chat, str);
                                }

                                catch { await botClient.SendTextMessageAsync(message.Chat, "Error in search"); }

                            }
                          //////////////////////

                            else if (message.Text.ToLower().Contains("/findlyrics"))
                            {

                                try
                                {
                                    var str = await _client.GetLyrics();
                                    await botClient.SendTextMessageAsync(message.Chat, str);
                                }

                                catch { await botClient.SendTextMessageAsync(message.Chat, "Error in search"); }

                            }

                            else if (message.Text.ToLower().Contains("/findtranslate"))
                            {


                                try
                                {

                                    var str = await _client.GetTranslate();
                                    //string abc = str.ToString();
                                    await botClient.SendTextMessageAsync(message.Chat, str);

                                }

                                catch (Exception e)
                                {
                                await botClient.SendTextMessageAsync(message.Chat, e.ToString());

                                }

                            }

                            else if (message.Text.ToLower().Contains("/save"))
                            {

                                try
                                {

                                     _client.AddtFav(message.Chat.Id.ToString());
                                  
                                    /*await*/ botClient.SendTextMessageAsync(message.Chat, "Last song have been saved");

                                }

                                catch (Exception e)
                                {
                                    /*await*/ botClient.SendTextMessageAsync(message.Chat, e.ToString());

                                }

                            }

                            else if (message.Text.ToLower().Contains("/list"))
                            {

                                try
                                {

                                    var str = /*await*/ _client.GetList(message.Chat.Id.ToString());

                                    /*await*/ botClient.SendTextMessageAsync(message.Chat, str.Result);

                                }

                                catch (Exception e)
                                {
                                    /*await*/ botClient.SendTextMessageAsync(message.Chat, "Error");

                                }

                            }


                            else if (message.Text.ToLower().Contains("/delete "))
                            {
                                

                                try
                                {

                                    var number = Convert.ToInt32(message.Text.Replace("/delete ", ""));


                                    /*await*/var responce = _client.Delete(message.Chat.Id.ToString(), number);

                                    /*await*/ botClient.SendTextMessageAsync(message.Chat, "your list has been updated");

                                }

                                catch (Exception e)
                                {
                                   /*await*/ botClient.SendTextMessageAsync(message.Chat, "nice try");

                                }

                            }




                        }
                        else if (message.Text == null)
                        {
                            await botClient.SendTextMessageAsync(message.Chat, "Incorrect imput try use /help");
                        }

                    }
                    catch {}
                   
                }

            }
            catch {}
            

        }
        


        public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
            
        }
        

       public  void start()
        {
            //Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            


           
        }
       
    }

}
