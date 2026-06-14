using Npgsql;
using NpgsqlTypes;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess
{
    public class CompatibiliteRepository
    {
        private readonly string _connectionString;

        public CompatibiliteRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Ajouter(Compatibilite compatibilite)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                CALL ajouter_compatibilite(
                    @id_animal,
                    @type_compatibilite,
                    @valeur,
                    @description
                );
            ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@id_animal", NpgsqlDbType.Integer)
                .Value = compatibilite.IdAnimal;

            command.Parameters.Add("@type_compatibilite", NpgsqlDbType.Varchar)
                .Value = compatibilite.TypeCompatibilite;

            command.Parameters.Add("@valeur", NpgsqlDbType.Varchar)
                .Value = compatibilite.Valeur;

            command.Parameters.Add("@description", NpgsqlDbType.Text)
                .Value = string.IsNullOrWhiteSpace(compatibilite.Description)
                    ? DBNull.Value
                    : compatibilite.Description;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Compatibilité ajoutée.");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }

        public void Supprimer(int idAnimal, string typeCompatibilite)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                CALL supprimer_compatibilite(
                    @id_animal,
                    @type_compatibilite
                );
            ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@id_animal", NpgsqlDbType.Integer)
                .Value = idAnimal;

            command.Parameters.Add("@type_compatibilite", NpgsqlDbType.Varchar)
                .Value = typeCompatibilite;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Compatibilité supprimée.");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }
    }
}