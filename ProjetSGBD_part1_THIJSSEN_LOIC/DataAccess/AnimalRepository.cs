using Npgsql;
using NpgsqlTypes;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NpgsqlTypes;
namespace ProjetSGBD_part1_THIJSSEN_LOIC.DataAccess
{
    public class AnimalRepository
    {
        private readonly string _connectionString;

        public AnimalRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Ajouter(Animal animal)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
        CALL ajouter_animal(
            @nom,
            @type_animal,
            @sexe,
            @date_naissance,
            @sterilise,
            @date_sterilisation,
            @couleurs,
            @description,
            @particularites,
            @etat_courant,
            @date_deces
        );
    ";

            using var command = new NpgsqlCommand(sql, connection);

            command.Parameters.Add("@nom", NpgsqlDbType.Varchar).Value = animal.Nom;
            command.Parameters.Add("@type_animal", NpgsqlDbType.Varchar).Value = animal.TypeAnimal;
            command.Parameters.Add("@sexe", NpgsqlDbType.Varchar).Value = animal.Sexe;

            command.Parameters.Add("@date_naissance", NpgsqlDbType.Date).Value = animal.DateNaissance;

            command.Parameters.Add("@sterilise", NpgsqlDbType.Boolean).Value = animal.Sterilise;

            command.Parameters.Add("@date_sterilisation", NpgsqlDbType.Date).Value =
                animal.DateSterilisation ?? (object)DBNull.Value;

            command.Parameters.Add("@couleurs", NpgsqlDbType.Varchar).Value = animal.Couleurs;
            command.Parameters.Add("@description", NpgsqlDbType.Text).Value = animal.Description;
            command.Parameters.Add("@particularites", NpgsqlDbType.Text).Value = animal.Particularites;
            command.Parameters.Add("@etat_courant", NpgsqlDbType.Varchar).Value = animal.EtatCourant;

            command.Parameters.Add("@date_deces", NpgsqlDbType.Date).Value =
                animal.DateDeces ?? (object)DBNull.Value;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Animal ajouté.");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }
        public void AfficherTout()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_tous_animaux();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["id_animal"]} | " +
                    $"Nom: {reader["nom"]} | " +
                    $"Type: {reader["type_animal"]} | " +
                    $"Sexe: {reader["sexe"]} | " +
                    $"État: {reader["etat_courant"]}"
                );
            }
        }
        public void AfficherUn(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_un_animal(@id);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["id_animal"]} | " +
                    $"Nom: {reader["nom"]} | " +
                    $"Type: {reader["type_animal"]} | " +
                    $"Sexe: {reader["sexe"]} | " +
                    $"État: {reader["etat_courant"]}"
                );
            }
            else
            {
                Console.WriteLine("Animal introuvable.");
            }
        }
        public void SupprimerUn(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "CALL supprimer_animal(@id);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

            try
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Animal supprimé.");
            }
            catch (PostgresException ex)
            {
                Console.WriteLine(ex.MessageText);
            }
        }
        public void ListerAnimauxRefuge()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM lister_animaux_refuge();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["id_animal"]} | " +
                    $"Nom: {reader["nom"]} | " +
                    $"Type: {reader["type_animal"]} | " +
                    $"Sexe: {reader["sexe"]} | " +
                    $"État: {reader["etat_courant"]}"
                );
            }
        }
        public List<Animal> GetAll()
        {
            List<Animal> animaux = new List<Animal>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_tous_animaux();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                animaux.Add(LireAnimal(reader));
            }

            return animaux;
        }
        public Animal? GetById(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM afficher_un_animal(@id);";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.Add("@id", NpgsqlDbType.Integer).Value = id;

            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                return LireAnimal(reader);
            }

            return null;
        }
        public List<Animal> GetAnimauxRefuge()
        {
            List<Animal> animaux = new List<Animal>();

            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM lister_animaux_refuge();";

            using var command = new NpgsqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                animaux.Add(LireAnimal(reader));
            }

            return animaux;
        }
        private Animal LireAnimal(NpgsqlDataReader reader)
        {
            return new Animal
            {
                IdAnimal = Convert.ToInt32(reader["id_animal"]),
                Nom = reader["nom"].ToString() ?? "",
                TypeAnimal = reader["type_animal"].ToString() ?? "",
                Sexe = reader["sexe"].ToString() ?? "",
                DateNaissance = reader["date_naissance"] == DBNull.Value
                ? DateTime.MinValue
                : ((DateOnly)reader["date_naissance"]).ToDateTime(TimeOnly.MinValue),
                Sterilise = reader["sterilise"] != DBNull.Value && Convert.ToBoolean(reader["sterilise"]),
                DateSterilisation = reader["date_sterilisation"] == DBNull.Value
                ? null
                : ((DateOnly)reader["date_sterilisation"]).ToDateTime(TimeOnly.MinValue),
                Couleurs = reader["couleurs"].ToString() ?? "",
                Description = reader["description"].ToString() ?? "",
                Particularites = reader["particularites"].ToString() ?? "",
                EtatCourant = reader["etat_courant"].ToString() ?? "",
                DateDeces = reader["date_deces"] == DBNull.Value
                ? null
                : ((DateOnly)reader["date_deces"]).ToDateTime(TimeOnly.MinValue),
            };
        }
    }
}
