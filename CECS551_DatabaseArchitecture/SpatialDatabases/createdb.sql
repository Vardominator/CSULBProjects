USE mysql;

CREATE DATABASE PublicSafety;

CREATE TABLE PublicSafety.Zones (
    ZoneID INT NOT NULL,
    ZoneName VARCHAR(100),
    SquadNumber INT,
    Region POLYGON,
    PRIMARY KEY (ZoneID)
);

CREATE TABLE PublicSafety.Officers (
    BadgeNumber INT NOT NULL,
    OfficerName VARCHAR(20),
    SquadNumber INT,
    Location POINT,
    PRIMARY KEY (BadgeNumber)
);

CREATE TABLE PublicSafety.Routes (
    RouteNumber INT NOT NULL,
    Route LINESTRING,
    PRIMARY KEY (RouteNumber)
);

CREATE TABLE PublicSafety.Incidents (
    IncidentID INT NOT NULL,
    Type VARCHAR(100),
    Location POINT,
    PRIMARY KEY (IncidentID)
);