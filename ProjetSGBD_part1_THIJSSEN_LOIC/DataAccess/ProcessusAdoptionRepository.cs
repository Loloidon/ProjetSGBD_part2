using Npgsql;
using NpgsqlTypes;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess
{
    public class ProcessusAdoptionRepository
    {
        private readonly string _connectionString;

        public ProcessusAdoptionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ajouter(ProcessusAdoption adoption)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                CALL ajouter_processus_adoption(
                    @date_demande,
                    @id_animal,
                    @id_contact
                );
            ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@date_demande", NpgsqlDbType.Date)
                .Value = adoption.DateDemande;

            command.Parameters.Add("@id_animal", NpgsqlDbType.Integer)
                .Value = adoption.IdAnimal;

            command.Parameters.Add("@id_contact", NpgsqlDbType.Integer)
                .Value = adoption.IdContact;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Demande d'adoption ajoutée.");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }

        public void Lister()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM lister_processus_adoptions();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID adoption: {reader["id_processus"]} | " +
                    $"Animal: {reader["nom_animal"]} | " +
                    $"Contact: {reader["nom_contact"]} {reader["prenom_contact"]} | " +
                    $"Date: {reader["date_demande"]} | " +
                    $"Statut: {reader["statutadoption"]}"
                );
            }
        }

        public void ModifierStatut(int idProcessus, string nouveauStatut)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                CALL modifier_statut_adoption(
                    @id_processus,
                    @nouveau_statut
                );
            ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@id_processus", NpgsqlDbType.Integer)
                .Value = idProcessus;

            command.Parameters.Add("@nouveau_statut", NpgsqlDbType.Varchar)
                .Value = nouveauStatut;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Statut modifié.");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }
    }
}