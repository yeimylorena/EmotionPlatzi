using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmoEmotion
    {
        public int Id { get; set; }
        //Probabilidad de que sea x oy emocion
        public float Score { get; set; }
        //Permite consultar el objeto padre
        public int EmoFaceId { get; set; }
        public EmoEmotionEnum EmotionType { get; set; }
        public virtual EmoFace Face { get; set; }
    }
}