CREATE MIGRATION m1ipxcgrn2hk2czw4ifokbefpz34rtwqrnta7iysja6kfoeirnqpva
    ONTO m1bgcs6uxree7uj24l2jrwski7ve23ummpz6cwlem5ouszyp7hrsvq
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
      ALTER PROPERTY Password {
          RENAME TO password;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY Role {
          RENAME TO role;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY Title {
          RENAME TO title;
      };
  };
  ALTER TYPE default::Contact {
      ALTER PROPERTY UserName {
          RENAME TO user_name;
      };
  };
};
