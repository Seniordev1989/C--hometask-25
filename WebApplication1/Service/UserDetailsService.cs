using Antlr.Runtime.Misc;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using WebApplication1.Context;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Service
{
    public class UserDetailsService
    {
        private readonly UserDetailsRepository _repository = new UserDetailsRepository();


        public List<UserDetails> GetAllUserDetails()
        {
            var result = _repository.GetAllUserDetails();

            return result;
        }
        public UserDetails GetById(int Id)
        {
            var result = _repository.GetById(Id);

            return result;
        }
        public bool Update(UserDetails usrdata)
        {
            var result = _repository.Update(usrdata);

            return result;
        }
        public bool Delete(int Id)
        {
            var result = _repository.Delete(Id);

            return result;
        }

        public object UploadCsv(HttpPostedFileBase file)
        {
            try
            {
                List<UserDetails> userDetails = new List<UserDetails>();
                using (var reader = new StreamReader(file.InputStream))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (values.Length > 0 && values[0] != "Identity")
                        {
                            int age = 0;
                            if (!int.TryParse(values[3], out _))
                            {
                                age = 0;

                            }
                            else
                            {
                                age = int.Parse(values[3]);
                            }
                            string mobileno = "";
                            if (values[5] != null && IsValidMobileNumber(values[5]))
                            {
                                mobileno = values[5];

                            }
                            else
                            {
                                mobileno = "";
                            }

                            string sex = "";
                            if (values[4] != null && !ContainsDigit(values[4]))
                            {
                                sex = values[4];

                            }
                            else
                            {
                                sex = "";
                            }
                            bool active = false;
                            if (!bool.TryParse(values[6], out _))
                            {
                                active = false;

                            }
                            else
                            {
                                active = bool.Parse(values[6]);
                            }
                            var record = new UserDetails
                            {
                                FirstName = values[1] != null ? values[1] : "",
                                Surname = values[2] != null ? values[2] : "",
                                Age = age,
                                Sex = sex,
                                Mobile = mobileno,
                                Active = active
                            };

                            userDetails.Add(record);
                            var result = _repository.Create(record);
                        }

                        //  records.Add(record);
                    }
                }
                return new { status = true, message = "Upload successfull!" };
            }
            catch (Exception ex)
            {
                return new { status = false, message = ex.Message };
            }
        }

        private bool ContainsDigit(string str)
        {
            return Regex.IsMatch(str, @"[\d]");
        }
        private bool IsValidMobileNumber(string str)
        {
            return Regex.IsMatch(str, @"^[\d+\s-]+$");
        }
    }
}