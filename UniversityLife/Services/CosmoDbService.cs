using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityLife.Models;

namespace UniversityLife.Services
{
    public class CosmoDbService : ICosomosDbService
    {
        private Microsoft.Azure.Cosmos.Container _Container;

        public CosmoDbService(
            CosmosClient dbClient,
            string databaseName,
            string containerName
            )
        {
            this._Container = dbClient.GetContainer(databaseName, containerName);
        }
        public async Task AddStudentAsync(Student student)
        {
            await this._Container.CreateItemAsync<Student>(student, new PartitionKey(student.Id));
        }

        public async Task DeleteStudentAsync(string id)
        {
            await this._Container.DeleteItemAsync<Student>(id, new PartitionKey(id));
        }

        public async Task<Student> GetStudentAsync(string id)
        {
            try
            {
                ItemResponse<Student> response = await this._Container.ReadItemAsync<Student>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync(string querString)
        {
            var query = this._Container.GetItemQueryIterator<Student>(new QueryDefinition(querString));
            List<Student> results = new List<Student>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

       

        public async Task UpdateStudentAsync(string id, Student student)
        {
            await this._Container.UpsertItemAsync<Student>(student, new PartitionKey(id));
        }

        List<Student>  ICosomosDbService.StudentList(string querString)
        {
            //IEnumerable<Student> students = (IEnumerable<Student>)GetStudentsAsync(querString);
            var stu = GetStudentsAsync(querString);
            List<Student> records = (List<Student>)stu.Result;
            
            return records;
        }
    }
}
