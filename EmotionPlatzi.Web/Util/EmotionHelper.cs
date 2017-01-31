﻿using EmotionPlatzi.Web.Models;
using Microsoft.ProjectOxford.Common.Contract;
using Microsoft.ProjectOxford.Emotion;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;

namespace EmotionPlatzi.Web.Util
{
    public class EmotionHelper
    {
        public EmotionServiceClient emoClient;

        public EmotionHelper(string key)
        {
            emoClient = new EmotionServiceClient(key);
        }

        public async Task<EmoPicture> DetectAndExtracFacesAsync(Stream imageStream)
        {
            Emotion[] emotions = await emoClient.RecognizeAsync(imageStream);

            var emoPicture = new EmoPicture();
            emoPicture.Faces = ExtractFaces(emotions,emoPicture);
            //Path

            return emoPicture;
            
        }

        private ObservableCollection<EmoFace> ExtractFaces(Emotion[] emotions, EmoPicture emoPicture)
        {
            //emoPicture.Faces = new ObservableCollection<EmoFace>();
            var listFaces = new ObservableCollection<EmoFace>();

            foreach (var emotion in emotions)
            {
                var emoFace = new EmoFace()
                {
                    X = emotion.FaceRectangle.Left,
                    Y = emotion.FaceRectangle.Top,
                    Width = emotion.FaceRectangle.Width,
                    Height = emotion.FaceRectangle.Height,
                    Picture = emoPicture,
                   
                };

                //emoPicture.Faces.Add(emoFace);
                emoFace.Emotions = ProcessEmotions(emotion.Scores,emoFace);
                listFaces.Add(emoFace);
            }
            return listFaces;
        }


        //Reflection o programación dinamica, analizar un objeto en tiempo de ejecución
        private ObservableCollection<EmoEmotion> ProcessEmotions(EmotionScores scores, EmoFace emoFace)
        {
            //Scores lo convirte en una lista de emotions
            var emotionList = new ObservableCollection<EmoEmotion>();
            //informacion generica
            var properties = scores.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var filterProperties = from p in properties
                                   where p.PropertyType == typeof(float)
                                   select p;

                                    //properties.Where(p => p.PropertyType == typeof(float));
            var emotype = EmoEmotionEnum.Undetermined;
            foreach (var prop in filterProperties)
            {
                if (Enum.TryParse<EmoEmotionEnum>(prop.Name, out emotype))
                    emotype = EmoEmotionEnum.Undetermined;

                var emoEmotion = new EmoEmotion();
                emoEmotion.Score = (float)prop.GetValue(scores);
                emoEmotion.EmotionType = emotype;
                emoEmotion.Face = emoFace;

                emotionList.Add(emoEmotion);
            }


            return emotionList;
        }
    }
}