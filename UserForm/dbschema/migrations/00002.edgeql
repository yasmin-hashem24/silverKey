CREATE MIGRATION m14dcyoq4kv4vvllo2cvbsohy665lon5s4xtzxiyipocr34vbpzkbq
    ONTO m1vd5fqpxgj7nyilbjqgdlx2g6dnwrf3hji3edmjoxh4jqi5uvtz3q
{
  CREATE TYPE default::Contact {
      CREATE REQUIRED PROPERTY DateOfBirth: std::str;
      CREATE REQUIRED PROPERTY Description: std::str;
      CREATE REQUIRED PROPERTY Email: std::str;
      CREATE REQUIRED PROPERTY FirstName: std::str;
      CREATE REQUIRED PROPERTY LastName: std::str;
      CREATE REQUIRED PROPERTY MarriageStatus: std::str;
      CREATE REQUIRED PROPERTY Title: std::str;
  };
  DROP TYPE default::Person;
};
