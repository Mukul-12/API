﻿using API.Models.DTO;

namespace API.Data
{
    public class EmployeeData
    {
        public static List<EmployeeDTO> EmployeeList = new List<EmployeeDTO>()
        {
            new EmployeeDTO(){Id = 1, Name = "Mukul", salary= 10000},
            new EmployeeDTO(){Id = 2, Name = "Ram", salary = 20000}
        };
    }
}
