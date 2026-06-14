using Npgsql;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess
{
    public class AccueilFamilleRepository
    {
        private readonly string _connectionString;

        public AccueilFamilleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ajouter(AccueilFamille accueilfamille)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
        CALL ajouter_accueil_famille(
            @date_debut,
            @date_fin,
            @id_animal,
            @id_contact
        );
    ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@date_debut", NpgsqlTypes.NpgsqlDbType.Date)
                .Value = accueilfamille.DateDebut;

            command.Parameters.Add("@date_fin", NpgsqlTypes.NpgsqlDbType.Date)
                .Value = accueilfamille.DateFin ?? (object)DBNull.Value;

            command.Parameters.Add("@id_animal", NpgsqlTypes.NpgsqlDbType.Integer)
                .Value = accueilfamille.IdAnimal;

            command.Parameters.Add("@id_contact", NpgsqlTypes.NpgsqlDbType.Integer)
                .Value = accueilfamille.IdContact;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Accueil famille ajouté !");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }

        public void ListerFamilleAnimalPasse(int idAnimal)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
        SELECT *
        FROM lister_familles_animal_passe(@id_animal);
    ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id_animal", idAnimal);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID accueil: {reader["id_accueil"]} | " +
                    $"Début: {reader["date_debut"]} | " +
                    $"Fin: {(reader["date_fin"] == DBNull.Value ? "En cours" : reader["date_fin"])} | " +
                    $"Contact: {reader["nom"]} {reader["prenom"]} | " +
                    $"GSM: {reader["gsm"]} | " +
                    $"Mail: {reader["mail"]}"
                );
            }
        }

        public void ListerAnimauxAccueillisFamille(int idContact)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                SELECT *
                FROM lister_animaux_accueillis_famille(@id_contact);
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id_contact", idContact);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                   $"ID accueil: {reader["id_accueil"]} | " +
                   $"Début: {reader["date_debut"]} | " +
                   $"Fin: {reader["date_fin"]} | " +
                   $"Animal: {reader["nom"]} | " +
                   $"Type: {reader["type_animal"]} | " +
                   $"Sexe: {reader["sexe"]}"
                );
            }
        }
    }
}