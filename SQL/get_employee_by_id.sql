CREATE OR REPLACE FUNCTION get_employee_by_id(emp_id INT)
RETURNS TABLE (
    Id INT,
    FirstName VARCHAR,
    MiddleName VARCHAR,
    LastName VARCHAR,
    Address VARCHAR,
    Phone VARCHAR,
    BirthDate DATE,
    HireDate DATE,
    Salary NUMERIC,
    DepartmentId INT,
    PositionId INT,
    CompanyId INT
)
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        e.Id,
        e.FirstName,
        e.MiddleName,
        e.LastName,
        e.Address,
        e.Phone,
        e.BirthDate,
        e.HireDate,
        e.Salary,
        e.DepartmentId,
        e.PositionId,
        e.CompanyId
    FROM public.Employee e
    WHERE e.Id = emp_id;
END;
$$ LANGUAGE plpgsql;
