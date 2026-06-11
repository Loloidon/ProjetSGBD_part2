using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;

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

            string sql = "SELECT id_vaccin, nom_vaccin FROM vaccin ORDER BY id_vaccin;";

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
                INSERT INTO vaccination
                (id_animal, id_vaccin, date_vaccination)
                VALUES
                (@id_animal, @id_vaccin, @date_vaccination);
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id_animal", idAnimal);
            command.Parameters.AddWithValue("@id_vaccin", idVaccin);
            command.Parameters.AddWithValue("@date_vaccination", dateVaccination);

            command.ExecuteNonQuery();

            Console.WriteLine("Vaccination ajoutée !");
        }

        public void AfficherVaccinsAnimal(int idAnimal)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            connection.Open();

            string sql = @"
                SELECT v.id_vaccin,
                       v.nom_vaccin,
                       CASE 
                           WHEN va.id_animal IS NULL THEN 'non fait'
                           ELSE 'fait'
                       END AS statut
                FROM vaccin v
                LEFT JOIN vaccination va 
                    ON v.id_vaccin = va.id_vaccin
                    AND va.id_animal = @id_animal
                ORDER BY v.id_vaccin;
            ";

            using var command = new NpgsqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id_animal", idAnimal);

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
    }
}