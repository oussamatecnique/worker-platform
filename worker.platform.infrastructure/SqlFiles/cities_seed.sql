-- Seed Regions
set identity_insert region on
INSERT INTO Region (Id, Name)
VALUES (1, 'Tanger-Tétouan-Al Hoceïma'),
       (2, 'Oriental'),
       (3, 'Fès-Meknès'),
       (4, 'Rabat-Salé-Kénitra'),
       (5, 'Béni Mellal-Khénifra'),
       (6, 'Casablanca-Settat'),
       (7, 'Marrakech-Safi'),
       (8, 'Drâa-Tafilalet'),
       (9, 'Souss-Massa'),
       (10, 'Guelmim-Oued Noun'),
       (11, 'Laâyoune-Sakia El Hamra'),
       (12, 'Dakhla-Oued Ed-Dahab');
set identity_insert region off


    set identity_insert city on

-- Seed Cities
INSERT INTO City (Id, Name, RegionId)
VALUES
-- Tanger-Tétouan-Al Hoceïma
(1, 'Tanger', 1),
(2, 'Tétouan', 1),
(3, 'Al Hoceïma', 1),

-- Oriental
(4, 'Oujda', 2),
(5, 'Berkane', 2),
(6, 'Nador', 2),

-- Fès-Meknès
(7, 'Fès', 3),
(8, 'Meknès', 3),
(9, 'Taza', 3),

-- Rabat-Salé-Kénitra
(10, 'Rabat', 4),
(11, 'Salé', 4),
(12, 'Kénitra', 4),

-- Béni Mellal-Khénifra
(13, 'Béni Mellal', 5),
(14, 'Khénifra', 5),
(15, 'Azilal', 5),

-- Casablanca-Settat
(16, 'Casablanca', 6),
(17, 'Settat', 6),
(18, 'El Jadida', 6),

-- Marrakech-Safi
(19, 'Marrakech', 7),
(20, 'Safi', 7),
(21, 'Essaouira', 7),

-- Drâa-Tafilalet
(22, 'Ouarzazate', 8),
(23, 'Errachidia', 8),
(24, 'Zagora', 8),

-- Souss-Massa
(25, 'Agadir', 9),
(26, 'Taroudant', 9),
(27, 'Tiznit', 9),

-- Guelmim-Oued Noun
(28, 'Guelmim', 10),
(29, 'Sidi Ifni', 10),
(30, 'Tan-Tan', 10),

-- Laâyoune-Sakia El Hamra
(31, 'Laâyoune', 11),
(32, 'Boujdour', 11),
(33, 'Tarfaya', 11),

-- Dakhla-Oued Ed-Dahab
(34, 'Dakhla', 12),
(35, 'Aousserd', 12);
    set identity_insert city off
