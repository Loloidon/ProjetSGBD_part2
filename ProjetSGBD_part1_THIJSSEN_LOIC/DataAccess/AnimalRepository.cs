using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;
using Npgsql;
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
                Insert INTO animal
                (nom,type_animal, sexe, date_naissance, sterilise,date_sterilisation, couleurs, description, particularites, etat_courant, date_deces )
                VALUES
                (@nom,@type_animal,@sexe,@date_naissance,@sterilise, @date_sterilisation,@couleurs, @description, @particularites, @etat_courant, @date_deces);
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@nom", animal.Nom);
            command.Parameters.AddWithValue("@type_animal", animal.TypeAnimal);
            command.Parameters.AddWithValue("@sexe", animal.Sexe);
            command.Parameters.AddWithValue("@date_naissance", animal.DateNaissance);
            command.Parameters.AddWithValue("@sterilise", animal.Sterilise);


            command.Parameters.AddWithValue("@date_sterilisation", animal.DateSterilisation ?? (object)DBNull.Value);
            
            command.Parameters.AddWithValue("@couleurs", animal.Couleurs);
            command.Parameters.AddWithValue("@description", animal.Description);
            command.Parameters.AddWithValue("@particularites", animal.Particularites);
            command.Parameters.AddWithValue("@etat_courant", animal.EtatCourant);
            
            command.Parameters.AddWithValue("@date_deces", animal.DateDeces ?? (object)DBNull.Value);
            
            

            command.ExecuteNonQuery();

            connection.Close();
        }
        public void AfficherTout()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM animal";

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
        public void  AfficherUn(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            string sql = @"
                SELECT *
                FROM animal
                WHERE id_animal = @id;
            ";
            using var command = new NpgsqlCommand(sql , connection);
            command.Parameters.AddWithValue("@id",id); 
            connection.Open();


            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                Console.WriteLine(
                    $"ID: {reader["id_animal"]} | " +
                    $"Nom: {reader["nom"]} | " +
                    $"Type: {reader["type_animal"]} | " +
                    $"Sexe: {reader["sexe"]} | " +
                    $"État: {reader["etat_courant"]}");
            }
            else
            {
                Console.WriteLine("Animal introuvable");
            }
                connection.Close();
        }
        public void SupprimerUn(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                        DELETE FROM animal
                        WHERE id_animal = @id;
                    ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);

            int nbLignes = command.ExecuteNonQuery();

            if (nbLignes > 0)
            {
                Console.WriteLine("Animal supprimé.");
            }
            else
            {
                Console.WriteLine("Animal introuvable.");
            }
        }
        public void ListerAnimauxRefuge()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM animal" +
                " WHERE etat_courant = @etat";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@etat", "refuge");
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
    }
}
