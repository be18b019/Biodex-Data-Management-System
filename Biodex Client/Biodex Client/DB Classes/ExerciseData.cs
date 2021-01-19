using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biodex_Client.DB_Classes
{
    class ExerciseData
    {
        public long nExerciseID { get; set; }
        public float[] nTorque { get; set; }
        public float[] nAngle { get; set; }
        public float[] nVelocity { get; set; }
        public string sMuscle { get; set; }
        public string sExercise { get; set; }      
        public string sRepetition { get; set; }

        public ExerciseData(long nExerciseID, float[] nTorque, float[] nAngle, float[] nVelocity, string sMuscle, string sExercise, string sRepetition)
        {
            this.nExerciseID = nExerciseID;
            this.nTorque = nTorque;
            this.nAngle = nAngle;
            this.nVelocity = nVelocity;
            this.sMuscle = sMuscle;
            this.sExercise = sExercise;
            this.sRepetition = sRepetition;
        }
    }
}
