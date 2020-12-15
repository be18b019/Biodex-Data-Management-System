using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biodex_Client.DB_Classes;

namespace Biodex_Client
{
    class DataAccessObject
    {
        private readonly string connString = "Host=localhost;Username=postgres;Password=Password123;Database=postgres";

        public async Task insertIntoBiodexReportAsync(BiodexReport biodexReport)
        {
            await using NpgsqlConnection conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            // Insert some data
            //await using (var cmd = new NpgsqlCommand("INSERT INTO data (some_field) VALUES (@p)", conn))
            using (var cmd = new NpgsqlCommand("INSERT INTO BiodexReport (Torque, AngularSpeed, Force, Exercise, Muscle, Repetition) VALUES (@t,@a,@f,@e,@m,@r)", conn))
            {
                cmd.Parameters.AddWithValue("t", biodexReport.Torque);
                cmd.Parameters.AddWithValue("a", biodexReport.AngularSpeed);
                cmd.Parameters.AddWithValue("f", biodexReport.Force);
                cmd.Parameters.AddWithValue("e", biodexReport.Exercise);
                cmd.Parameters.AddWithValue("m", biodexReport.Muscle);
                cmd.Parameters.AddWithValue("r", biodexReport.Repetition);
                await cmd.ExecuteNonQueryAsync();
            }
        }


    }
}
