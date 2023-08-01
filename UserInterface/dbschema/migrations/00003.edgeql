CREATE MIGRATION m1bgcs6uxree7uj24l2jrwski7ve23ummpz6cwlem5ouszyp7hrsvq
    ONTO m17hnmqtxrazhyapfb3pb7vwbaph6f5hdgbxellrxkv4t4savc6cia
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY Password: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
