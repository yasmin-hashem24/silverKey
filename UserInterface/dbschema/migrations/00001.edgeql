CREATE MIGRATION m1csqhmz35sjouc6mnqis5rm7od47ijzzgb3ypxpzr5pdn5ravv2ia
    ONTO initial
{
  CREATE TYPE default::Contact {
      CREATE REQUIRED PROPERTY DateOfBirth: std::str;
      CREATE REQUIRED PROPERTY Description: std::str;
      CREATE REQUIRED PROPERTY Email: std::str;
      CREATE REQUIRED PROPERTY FirstName: std::str;
      CREATE REQUIRED PROPERTY LastName: std::str;
      CREATE REQUIRED PROPERTY MarriageStatus: std::bool;
      CREATE REQUIRED PROPERTY Title: std::str;
  };
};
