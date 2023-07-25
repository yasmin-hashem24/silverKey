CREATE MIGRATION m1vd5fqpxgj7nyilbjqgdlx2g6dnwrf3hji3edmjoxh4jqi5uvtz3q
    ONTO initial
{
  CREATE TYPE default::Person {
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY dob: std::datetime;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY marriage_status: std::bool;
      CREATE REQUIRED PROPERTY title: std::str;
  };
};
