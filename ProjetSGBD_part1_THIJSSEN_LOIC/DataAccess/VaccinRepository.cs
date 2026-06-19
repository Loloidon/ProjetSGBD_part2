using Npgsql;
using NpgsqlTypes;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess
{
    public class VaccinRepository
    {
        private readonly string _connectionString;

        public VaccinRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void AfficherVaccins()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_vaccins();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id_vaccin"]} | Vaccin: {reader["nom_vaccin"]}");
            }
        }

        public void AjouterVaccination(int idAnimal, int idVaccin, DateTime dateVaccination)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                CALL ajouter_vaccination(
                    @id_animal,
                    @id_vaccin,
                    @date_vaccination
                );
            ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@id_animal", NpgsqlDbType.Integer).Value = idAnimal;
            command.Parameters.Add("@id_vaccin", NpgsqlDbType.Integer).Value = idVaccin;
            command.Parameters.Add("@date_vaccination", NpgsqlDbType.Date).Value = dateVaccination;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Vaccination ajoutée !");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }

        public void AfficherVaccinsAnimal(int idAnimal)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_vaccins_animal(@id_animal);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id_animal", NpgsqlDbType.Integer).Value = idAnimal;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["id_vaccin"]} | " +
                    $"Vaccin: {reader["nom_vaccin"]} | " +
                    $"Statut: {reader["statut"]}"
                );
            }
        }

        public List<Vaccin> GetAll()
        {
            List<Vaccin> vaccins = new List<Vaccin>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_vaccins();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                vaccins.Add(new Vaccin
                {
                    IdVaccin = Convert.ToInt32(reader["id_vaccin"]),
                    NomVaccin = reader["nom_vaccin"].ToString() ?? ""
                });
            }

            return vaccins;
        }

        public List<Vaccin> GetVaccinsAnimal(int idAnimal)
        {
            List<Vaccin> vaccins = new List<Vaccin>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_vaccins_animal(@id_animal);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id_animal", NpgsqlDbType.Integer).Value = idAnimal;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                vaccins.Add(new Vaccin
                {
                    IdVaccin = Convert.ToInt32(reader["id_vaccin"]),
                    NomVaccin = reader["nom_vaccin"].ToString() ?? "",
                    Statut = reader["statut"].ToString() ?? ""
                });
            }

            return vaccins;
        }
    }
}