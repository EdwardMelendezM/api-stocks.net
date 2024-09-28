using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;

namespace api.Interface
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetComments();
        Task<Comment?> GetCommentById(int id);
        Task<Comment> CreateComment(Comment comment);
        Task<Comment> UpdateComment(Comment comment);
        Task<Comment> DeleteComment(Comment comment);
        
    }
}