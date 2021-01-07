using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class ExerciseData
    {
        public long Exercise_id { get; set; }
        public float[] Torque { get; set; }
        public float[] Angle { get; set; }
        public float[] Velocity { get; set; }
        public string Muscle { get; set; }
        public string Exercise { get; set; }      
        public string Repetition { get; set; }

        public ExerciseData(long exercise_id, float[] torque, float[] angle, float[] velocity, string muscle, string exercise, string repetition)
        {
            Exercise_id = exercise_id;
            Torque = torque;
            Angle = angle;
            Velocity = velocity;
            Muscle = muscle;
            Exercise = exercise;
            Repetition = repetition;
        }
    }
}
