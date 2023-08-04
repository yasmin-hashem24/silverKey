module default {
  type Contact {
    required property first_name -> str;
    required property last_name -> str;
    required property password -> str;
    required property email -> str;
    required property title -> str;
    required property description -> str;
    required property date_of_birth -> str;
    required property role -> str;
    required property user_name -> str;
    required property marriage_status -> bool;
  }
}