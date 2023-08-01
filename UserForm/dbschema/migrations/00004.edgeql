CREATE MIGRATION m12adndfdxck65ybu4anqqe6jqw26dysjo2gjqijomm77f6m5yr67q
    ONTO m1ihgwwkh5rr4vildodxpg52o6haaofr5z7gapf74rdg2c3cfvzhoa
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY DateOfBirth {
          RENAME TO date_of_birth;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY Description {
          RENAME TO description;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY Email {
          RENAME TO email;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY FirstName {
          RENAME TO first_name;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY LastName {
          RENAME TO last_name;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY MarriageStatus {
          RENAME TO marriage_status;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY Title {
          RENAME TO title;
      };
  };
};
