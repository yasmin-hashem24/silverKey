CREATE MIGRATION m1ihgwwkh5rr4vildodxpg52o6haaofr5z7gapf74rdg2c3cfvzhoa
    ONTO m14dcyoq4kv4vvllo2cvbsohy665lon5s4xtzxiyipocr34vbpzkbq
{
  ALTER TYPE default::Contact {
      ALTER PROPERTY MarriageStatus {
          SET TYPE std::bool USING (<std::bool>.MarriageStatus);
      };
  };
};
