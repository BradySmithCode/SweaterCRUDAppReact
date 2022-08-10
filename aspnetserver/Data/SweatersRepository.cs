using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal static class SweatersRepository
    {
        internal async static Task<List<Sweater>> GetSweatersAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Sweaters.ToListAsync();
            }
        }

        internal async static Task<Sweater> GetSweaterByIdAsync(int sweaterId)
        {
            using (var db = new AppDBContext())
            {
                return await db.Sweaters.FirstOrDefaultAsync(sweater => sweater.SweaterId == sweaterId);
            }
        }

        internal async static Task<bool> CreateSweaterAsync(Sweater sweaterToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Sweaters.AddAsync(sweaterToCreate);

                    return await db.SaveChangesAsync() >= 1;

                } catch (Exception e)
                {
                    return false;
                }
            }
        }

        internal async static Task<bool> UpdateSweaterAsync(Sweater sweaterToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Sweaters.Update(sweaterToUpdate);

                    return await db.SaveChangesAsync() >= 1;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
        internal async static Task<bool> DeleteSweaterAsync(int sweaterId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Sweater sweaterToDelete = await GetSweaterByIdAsync(sweaterId);

                    db.Remove(sweaterToDelete);

                    return await db.SaveChangesAsync() >= 1;

                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

    }
}
