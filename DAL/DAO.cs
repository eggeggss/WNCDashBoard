using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{

    public class ItemRepository : IItemRepository
    {

        public EFAdapter _adapter;

        public ItemRepository()
        {
            _adapter = new EFAdapter();

        }


        public bool Delete(Item t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.dt_update = DateTime.Now;
                        t.stat_void = 1;
                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();
                        return true;
                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemRepositry Delete fail:{ex.Message}");
            }
        }

        public Item Get(string name)
        {
            try
            {
                Func<Item> func = new Func<Item>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.Item.Where(e => e.item_name == name
                        && e.stat_void == 0).FirstOrDefault();

                    }
                });

                return _adapter.Catch<Item>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemRepositry Get fail:{ex.Message}");
            }
        }

        public IEnumerable<Item> GetAll()
        {
            try
            {
                Func<IEnumerable<Item>> func = new Func<IEnumerable<Item>>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.Item.Where(e =>
                        e.stat_void == 0).ToList();

                    }
                });

                return _adapter.Catch<IEnumerable<Item>>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemRepositry GetAll fail:{ex.Message}");
            }
        }

        public bool Insert(Item t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {
                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;
                        t.dt_create = DateTime.Now;
                        db.Entry(t).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemRepositry GetAll fail:{ex.Message}");
            }
        }

        public int InsertReturnId(Item t)
        {
            try
            {
                Func<int> func = new Func<int>(() =>
                {
                    using (var db = new Dashboard1Entities())
                    {
                        t.stat_void = 0;
                        t.dt_create = DateTime.Now;
                        db.Entry(t).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        return t.id_item;
                    }
                });

                return _adapter.Catch<int>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemRepositry GetAll fail:{ex.Message}");
            }
        }

        public bool Update(Item t)
        {

            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;
                        t.dt_update = DateTime.Now;
                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemRepositry GetAll fail:{ex.Message}");
            }
        }
    }


    public class ItemGroupRelRepository : IItemGroupRelRepository
    {

        public EFAdapter _adapter;

        public ItemGroupRelRepository()
        {
            _adapter = new EFAdapter();

        }

        public bool Delete(ItemGroupRel t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 1;
                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;

                        db.SaveChanges();
                        return true;
                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemGroupRelRepositry Del fail:{ex.Message}");
            }
        }

        public ItemGroupRel Get(string name)
        {
            try
            {
                Func<ItemGroupRel> func = new Func<ItemGroupRel>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.ItemGroupRel.Where(e => e.stat_void == 0).FirstOrDefault();

                    }
                });

                return _adapter.Catch<ItemGroupRel>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemGroupRelRepositry Get fail:{ex.Message}");
            }
        }

        public IEnumerable<ItemGroupRel> GetAll()
        {
            try
            {
                Func<IEnumerable<ItemGroupRel>> func = new Func<IEnumerable<ItemGroupRel>>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.ItemGroupRel.Where(e =>
                        e.stat_void == 0).ToList();

                    }
                });

                return _adapter.Catch<IEnumerable<ItemGroupRel>>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemGroupRelRepositry GetAll fail:{ex.Message}");
            }
        }

        public bool Insert(ItemGroupRel t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;
                        db.Entry(t).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemGroupRelRepositry GetAll fail:{ex.Message}");
            }
        }

        public bool Update(ItemGroupRel t)
        {

            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;

                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ItemGroupRelRepositry GetAll fail:{ex.Message}");
            }
        }
    }


    public class GroupRepository : IGroupRepository
    {
        public EFAdapter _adapter;

        public GroupRepository()
        {
            _adapter = new EFAdapter();
        }

        public bool Delete(Group t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 1;
                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"GroupRepositry Del fail:{ex.Message}");
            }
        }

        public Group Get(string name)
        {
            try
            {
                Func<Group> func = new Func<Group>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.Group.Where(e => e.stat_void == 0).FirstOrDefault();

                    }
                });

                return _adapter.Catch<Group>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"GroupRelRepositry Get fail:{ex.Message}");
            }
        }

        public IEnumerable<Group> GetAll()
        {
            try
            {
                Func<IEnumerable<Group>> func = new Func<IEnumerable<Group>>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.Group.Where(e =>
                        e.stat_void == 0).ToList();

                    }
                });

                return _adapter.Catch<IEnumerable<Group>>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"GroupRelRepositry GetAll fail:{ex.Message}");
            }
        }

        public bool Insert(Group t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;
                        db.Entry(t).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"GroupRepositry Insert fail:{ex.Message}");
            }
        }

        public bool Update(Group t)
        {

            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;

                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"GroupRepositry Update fail:{ex.Message}");
            }
        }
    }


    public class TabTemplateRepository : ITabTemplateRepository
    {
        public EFAdapter _adapter;

        public TabTemplateRepository()
        {
            _adapter = new EFAdapter();
        }

        public bool Delete(TabTemplate t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 1;
                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"TabTemplate Del fail:{ex.Message}");
            }
        }

        public TabTemplate Get(string name)
        {
            try
            {
                Func<TabTemplate> func = new Func<TabTemplate>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.TabTemplate.Where(e =>e.tab_name==name 
                        &&  e.stat_void == 0).FirstOrDefault();

                    }
                });

                return _adapter.Catch<TabTemplate>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"TabTemplate Get fail:{ex.Message}");
            }
        }

        public IEnumerable<TabTemplate> GetAll()
        {
            try
            {
                Func<IEnumerable<TabTemplate>> func = new Func<IEnumerable<TabTemplate>>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.TabTemplate.Where(e =>
                        e.stat_void == 0).ToList();

                    }
                });

                return _adapter.Catch<IEnumerable<TabTemplate>>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"TabTemplate GetAll fail:{ex.Message}");
            }
        }

        public bool Insert(TabTemplate t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;
                        db.Entry(t).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"TabTemplate Insert fail:{ex.Message}");
            }
        }

        public bool Update(TabTemplate t)
        {

            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;

                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"TabTemplate Update fail:{ex.Message}");
            }
        }
    }

    public class UsersRepository : IUsersRepository
    {
        public EFAdapter _adapter;

        public UsersRepository()
        {
            _adapter = new EFAdapter();
        }


        public bool Delete(Users t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {

                        t.stat_void = 1;
                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"Users Del fail:{ex.Message}");
            }
        }

        public Users Get(string name)
        {
            try
            {
                Func<Users> func = new Func<Users>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        return db.Users.Where(e => e.stat_void == 0).FirstOrDefault();

                    }
                });

                return _adapter.Catch<Users>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"Users Get fail:{ex.Message}");
            }
        }

        public IEnumerable<Users> GetAll()
        {
            try
            {
                Func<IEnumerable<Users>> func = new Func<IEnumerable<Users>>(() =>
                {
                    using (var db = new Dashboard1Entities())
                    {

                        return db.Users.Where(e =>
                        e.stat_void == 0).ToList();

                    }
                });

                return _adapter.Catch<IEnumerable<Users>>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"Users GetAll fail:{ex.Message}");
            }
        }

        public bool Insert(Users t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;
                        db.Entry(t).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"Users Insert fail:{ex.Message}");
            }
        }

        public bool Update(Users t)
        {

            try
            {
                Func<bool> func = new Func<bool>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {


                        t.stat_void = 0;

                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"Users Update fail:{ex.Message}");
            }
        }
    }

    public class UsersGroupRelRepository : IUsersGroupRelRepository
    {
        public EFAdapter _adapter;

        public UsersGroupRelRepository()
        {
            _adapter = new EFAdapter();
        }

        public bool Delete(UsersGroupsRel t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {
                    using (var db = new Dashboard1Entities())
                    {
                        t.stat_void = 1;
                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;
                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"UsersGroupRel Del fail:{ex.Message}");
            }
        }

        public UsersGroupsRel Get(string name)
        {
            try
            {
                Func<UsersGroupsRel> func = new Func<UsersGroupsRel>(() =>
                {
                    using (var db = new Dashboard1Entities())
                    {
                        return db.UsersGroupsRel.Where(e => e.stat_void == 0).FirstOrDefault();

                    }
                });

                return _adapter.Catch<UsersGroupsRel>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"UsersGroupsRel Get fail:{ex.Message}");
            }
        }

        public IEnumerable<UsersGroupsRel> GetAll()
        {
            try
            {
                Func<IEnumerable<UsersGroupsRel>> func = new Func<IEnumerable<UsersGroupsRel>>(() =>
                {

                    using (var db = new Dashboard1Entities())
                    {
                        return db.UsersGroupsRel.Where(e =>
                        e.stat_void == 0).ToList();
                    }
                });

                return _adapter.Catch<IEnumerable<UsersGroupsRel>>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"UsersGroupsRel GetAll fail:{ex.Message}");
            }
        }

        public bool Insert(UsersGroupsRel t)
        {
            try
            {
                Func<bool> func = new Func<bool>(() =>
                {
                    using (var db = new Dashboard1Entities())
                    {
                        t.stat_void = 0;
                        db.Entry(t).State = System.Data.Entity.EntityState.Added;
                        db.SaveChanges();
                        return true;
                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"UsersGroupsRel Insert fail:{ex.Message}");
            }
        }

        public bool Update(UsersGroupsRel t)
        {

            try
            {
                Func<bool> func = new Func<bool>(() =>
                {
                    using (var db = new Dashboard1Entities())
                    {
                        t.stat_void = 0;

                        db.Entry(t).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return true;

                    }
                });

                return _adapter.Catch<bool>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"UsersGroupsRel Update fail:{ex.Message}");
            }
        }
    }


    public class ExtentRepository: IExtentRepository
    {
        public EFAdapter _adapter;

        public ExtentRepository()
        {
            _adapter = new EFAdapter();
        }
        public IEnumerable<zp_get_not_exists_group_Result> GetHiddenClass(string empno)
        {
            try
            {
                Func<IEnumerable<zp_get_not_exists_group_Result>> func = new Func<IEnumerable<zp_get_not_exists_group_Result>>(() =>
                {
                    using (var db = new Dashboard1Entities())
                    {
                        return db.zp_get_not_exists_group(empno).ToList();
                        
                    }
                });

                return _adapter.Catch<IEnumerable<zp_get_not_exists_group_Result>>(func);
            }
            catch (Exception ex)
            {
                throw new Exception($"ExtentRepository GetHiddenClass fail:{ex.Message}");
            }

        }
    }

}
