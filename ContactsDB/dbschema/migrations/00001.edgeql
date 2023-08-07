CREATE MIGRATION m1n6taz2ydfq36yi3wdh3ihk4ldigvyo45ulexguentxzza6rwydrq
    ONTO initial
{
  CREATE TYPE default::Contact {
      CREATE REQUIRED PROPERTY date_of_birth: std::str;
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY marriage_status: std::bool;
      CREATE REQUIRED PROPERTY password: std::str;
      CREATE REQUIRED PROPERTY role: std::str;
      CREATE REQUIRED PROPERTY title: std::str;
      CREATE REQUIRED PROPERTY user_name: std::str;
  };
};
