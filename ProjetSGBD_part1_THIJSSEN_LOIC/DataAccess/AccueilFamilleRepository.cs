using Npgsql;
using NpgsqlTypes;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;

namespace ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess
{
    public class AccueilFamilleRepository
    {
        //avant WPF
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
        private DateTime LireDate(object value)
        {
            return value is DateOnly d ? d.ToDateTime(TimeOnly.MinValue) : Convert.ToDateTime(value);
        }

        private DateTime? LireDateNullable(object value)
        {
            if (value == DBNull.Value) return null;
            return value is DateOnly d ? d.ToDateTime(TimeOnly.MinValue) : Convert.ToDateTime(value);
        }

        public List<AccueilFamille> GetFamillesAnimalPasse(int idAnimal)
        {
            List<AccueilFamille> accueils = new List<AccueilFamille>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM lister_familles_animal_passe(@id_animal);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id_animal", NpgsqlDbType.Integer).Value = idAnimal;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                accueils.Add(new AccueilFamille
                {
                    IdAccueil = Convert.ToInt32(reader["id_accueil"]),
                    DateDebut = LireDate(reader["date_debut"]),
                    DateFin = LireDateNullable(reader["date_fin"]),
                    Nom = reader["nom"].ToString() ?? "",
                    Prenom = reader["prenom"].ToString() ?? "",
                    Gsm = reader["gsm"].ToString() ?? "",
                    Mail = reader["mail"].ToString() ?? ""
                });
            }

            return accueils;
        }

        public List<AccueilFamille> GetAnimauxAccueillisFamille(int idContact)
        {
            List<AccueilFamille> accueils = new List<AccueilFamille>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM lister_animaux_accueillis_famille(@id_contact);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id_contact", NpgsqlDbType.Integer).Value = idContact;

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                accueils.Add(new AccueilFamille
                {
                    IdAccueil = Convert.ToInt32(reader["id_accueil"]),
                    DateDebut = LireDate(reader["date_debut"]),
                    DateFin = LireDateNullable(reader["date_fin"]),
                    Nom = reader["nom"].ToString() ?? "",
                    TypeAnimal = reader["type_animal"].ToString() ?? "",
                    Sexe = reader["sexe"].ToString() ?? ""
                });
            }

            return accueils;
        }
    }
}