# Delete Rows

This script deletes one or more rows from a table in a DataMiner element. It is designed to be used with GQI (Generic Query Interface) or as a standalone automation script.

## Input Parameters

The script requires the following parameters:

| Name | Description | Format |
|------|-------------|--------|
| Element Identifier | The element containing the table | Element name OR `DataMinerID/ElementID` (e.g., `123/456`) |
| Table Identifier | The parameter ID of the table | Integer value (e.g., `1000`) |
| Primary Key(s) | The primary key(s) of the row(s) to delete | Comma or semicolon-separated list (e.g., `key1,key2,key3`) |

### Parameter Details

- **Element Identifier**: Can be specified in two ways:
  - By element name (e.g., `MyElement`)
  - By DataMiner ID and Element ID (e.g., `123/456`)
  
- **Table Identifier**: Must be a valid integer representing the parameter ID of the table in the element's protocol.

- **Primary Key(s)**: Multiple row keys can be provided, separated by commas (`,`) or semicolons (`;`). Leading and trailing whitespace will be automatically trimmed.

These parameters can also be filled in via feeds from a GQI query, allowing for dynamic input based on query results.

## Error Handling

The script will exit with an error message if:
- The element identifier is empty or invalid
- The table ID is empty, whitespace, or not a valid integer
- No valid row keys are provided
- The element cannot be found in the DataMiner System
- The element is not started or has not completed its startup routine
- The specified table does not exist in the element's protocol
- The user does not have permission to delete rows from the specified table
- The script encounters an error while attempting to delete the specified rows
- An unexpected exception occurs during execution
