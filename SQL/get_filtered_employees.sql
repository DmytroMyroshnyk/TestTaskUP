CREATE OR REPLACE FUNCTION get_filtered_employees(dept_id INT DEFAULT NULL, name_search TEXT DEFAULT NULL)
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
    CompanyId INT,
    DepartmentName VARCHAR,
    PositionName VARCHAR,
    CompanyName VARCHAR
)
AS $$
BEGIN
    RETURN QUERY
    SELECT 
        e.*, 
        d.Name AS DepartmentName,
        p.Name AS PositionName,
        c.Name AS CompanyName
    FROM public.Employee e
    JOIN public.Department d ON e.DepartmentId = d.Id
    JOIN public.Position p ON e.PositionId = p.Id
    JOIN public.Company c ON e.CompanyId = c.Id
    WHERE (dept_id IS NULL OR e.DepartmentId = dept_id)
      AND (
        name_search IS NULL OR
        e.FirstName ILIKE '%' || name_search || '%' OR
        e.LastName ILIKE '%' || name_search || '%' OR
        e.MiddleName ILIKE '%' || name_search || '%'
    )
	ORDER BY e.Id;
END;
$$ LANGUAGE plpgsql;
