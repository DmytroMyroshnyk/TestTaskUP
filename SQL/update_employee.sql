CREATE OR REPLACE PROCEDURE update_employee(
    p_id INT,
    p_firstname VARCHAR,
    p_middlename VARCHAR,
    p_lastname VARCHAR,
    p_address VARCHAR,
    p_phone VARCHAR,
    p_birthdate DATE,
    p_hiredate DATE,
    p_salary NUMERIC,
    p_department_id INT,
    p_position_id INT
)
AS $$
BEGIN
    UPDATE public.Employee
    SET
        FirstName = p_firstname,
        MiddleName = p_middlename,
        LastName = p_lastname,
        Address = p_address,
        Phone = p_phone,
        BirthDate = p_birthdate,
        HireDate = p_hiredate,
        Salary = p_salary,
        DepartmentId = p_department_id,
        PositionId = p_position_id
    WHERE Id = p_id;
END;
$$ LANGUAGE plpgsql;
