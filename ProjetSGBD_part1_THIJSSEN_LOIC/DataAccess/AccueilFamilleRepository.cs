using Npgsql;
using ProjetSGBD_part1_THIJSSEN_LOIC.Metier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Insert INTO accueil_famille
                (date_debut, date_fin, id_animal, id_contact )
                VALUES
                (@date_debut, @date_fin, @id_animal, @id_contact);
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@date_debut", accueilfamille.DateDebut);
            command.Parameters.AddWithValue("@date_fin", accueilfamille.DateFin ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@id_animal", accueilfamille.IdAnimal);
            command.Parameters.AddWithValue("@id_contact", accueilfamille.IdContact);
            



            command.ExecuteNonQuery();

            connection.Close();
        }

        public void ListerFamilleAnimalPasse(int idAnimal)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
        SELECT af.id_accueil, af.date_debut, af.date_fin,
               c.nom, c.prenom, c.gsm, c.mail
        FROM accueil_famille af
        JOIN contact c ON af.id_contact = c.id_contact
        WHERE af.id_animal = @id_animal;
    ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id_animal", idAnimal);

            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine(
                    $"ID accueil: {reader["id_accueil"]} | " +
                    $"Début: {reader["date_debut"]} | " +
                    $"Fin: {reader["date_fin"]} | " +
                    $"Contact: {reader["nom"]} {reader["prenom"]} | " +
                    $"GSM: {reader["gsm"]} | " +
                    $"Mail: {reader["mail"]}"
                );
            }
        }
        public void ListerAnimauxAcceuillisFamille(int idContact)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
        SELECT af.id_accueil, af.date_debut, af.date_fin,
        a.nom, a.type_animal, a.sexe
        FROM accueil_famille af
        JOIN animal a ON af.id_animal = a.id_animal
        WHERE af.id_contact = @id_contact;
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
