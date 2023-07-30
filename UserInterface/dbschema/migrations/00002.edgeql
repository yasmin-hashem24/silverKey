CREATE MIGRATION m17hnmqtxrazhyapfb3pb7vwbaph6f5hdgbxellrxkv4t4savc6cia
    ONTO m1csqhmz35sjouc6mnqis5rm7od47ijzzgb3ypxpzr5pdn5ravv2ia
{
  ALTER TYPE default::Contact {
      CREATE REQUIRED PROPERTY Role: std::str {
          SET REQUIRED USING (<std::str>{});
      };
      CREATE REQUIRED PROPERTY UserName: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
