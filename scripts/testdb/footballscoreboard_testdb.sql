SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: football; Type: SCHEMA; Schema: -; Owner: postgres
--

CREATE SCHEMA football;

ALTER SCHEMA football OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: game; Type: TABLE; Schema: football; Owner: postgres
--

CREATE TABLE football.game (
    id integer NOT NULL,
    week integer NOT NULL,
    home_team character varying(3) NOT NULL,
    away_team character varying(3) NOT NULL,
    state character varying(10),
    quarter integer,
    quarter_seconds_remaining integer
);


ALTER TABLE football.game OWNER TO postgres;

--
-- Name: play; Type: TABLE; Schema: football; Owner: postgres
--

CREATE TABLE football.play (
    id integer NOT NULL,
    play_id integer NOT NULL,
    game_id integer NOT NULL,
    week integer NOT NULL,
    home_team character varying(3) NOT NULL,
    home_score integer NOT NULL,
    away_team character varying(3) NOT NULL,
    away_score integer NOT NULL,
    quarter integer NOT NULL,
    quarter_seconds_remaining integer,
    half_seconds_remaining integer,
    game_seconds_remaining integer,
    game_date character varying(10) NOT NULL,
    game_half character varying(8) NOT NULL,
    description character varying(1000) NOT NULL,
    play_time character varying(10) NOT NULL,
    side_of_field character varying(3) NOT NULL,
    yard_line character varying(6) NOT NULL,
    play_type character varying(20),
    down integer,
    goal_to_go integer,
    yardline_100 integer,
    drive integer NOT NULL,
    yards_to_go integer NOT NULL,
    quarter_end boolean NOT NULL,
    touchdown boolean NOT NULL,
    shotgun boolean NOT NULL,
    no_huddle boolean NOT NULL,
    qb_dropback boolean NOT NULL,
    qb_kneel boolean NOT NULL,
    qb_spike boolean NOT NULL,
    qb_scramble boolean NOT NULL,
    qb_hit boolean NOT NULL,
    first_down_pass boolean NOT NULL,
    first_down_rush boolean NOT NULL,
    first_down_penalty boolean NOT NULL,
    third_down_converted boolean NOT NULL,
    third_down_failed boolean NOT NULL,
    fourth_down_converted boolean NOT NULL,
    fourth_down_failed boolean NOT NULL,
    pass_attempt boolean NOT NULL,
    pass_touchdown boolean NOT NULL,
    complete_pass boolean NOT NULL,
    incomplete_pass boolean NOT NULL,
    interception boolean NOT NULL,
    rush_attempt boolean NOT NULL,
    rush_touchdown boolean NOT NULL,
    fumble boolean NOT NULL,
    fumble_forced boolean NOT NULL,
    fumble_not_forced boolean NOT NULL,
    fumble_lost boolean NOT NULL,
    fumble_out_of_bounds boolean NOT NULL,
    touchback boolean NOT NULL,
    return_touchdown boolean NOT NULL,
    punt_attempt boolean NOT NULL,
    punt_blocked boolean NOT NULL,
    kickoff_attempt boolean NOT NULL,
    own_kickoff_recovery boolean NOT NULL,
    own_kickoff_recovery_td boolean NOT NULL,
    extra_point_attempt boolean NOT NULL,
    two_point_attempt boolean NOT NULL,
    field_goal_attempt boolean NOT NULL,
    solo_tackle boolean NOT NULL,
    assist_tackle boolean NOT NULL,
    tackle_for_loss boolean NOT NULL,
    sack boolean NOT NULL,
    safety boolean NOT NULL,
    penalty boolean NOT NULL,
    replay_or_challenge boolean NOT NULL,
    defensive_two_point_attempt boolean NOT NULL,
    defensive_two_point_conv boolean NOT NULL,
    defensive_extra_point_attempt boolean NOT NULL,
    defensive_extra_point_conv boolean NOT NULL,
    posteam character varying(3) NOT NULL,
    posteam_type character varying(4) NOT NULL,
    posteam_score integer,
    defteam character varying(4) NOT NULL,
    defteam_score integer,
    td_team character varying(3),
    net_yards integer NOT NULL,
    yards_gained integer NOT NULL,
    pass_length character varying(5),
    pass_location character varying(6),
    air_yards integer,
    yards_after_catch integer,
    run_location character varying(6),
    run_gap character varying(6),
    field_goal_result character varying(7),
    extra_point_result character varying(7),
    two_point_conv_result character varying(7),
    return_team character varying(3),
    return_yards integer,
    kick_distance integer,
    penalty_team character varying(3),
    penalty_type character varying(80),
    penalty_yards integer,
    replay_or_challenge_result character varying(8),
    timeout boolean NOT NULL,
    timeout_team character varying(3),
    home_timeouts_remaining integer NOT NULL,
    away_timeouts_remaining integer NOT NULL,
    posteam_timeouts_remaining integer,
    defteam_timeouts_remaining integer
);


ALTER TABLE football.play OWNER TO postgres;

--
-- Name: stat; Type: TABLE; Schema: football; Owner: postgres
--

CREATE TABLE football.stat (
    game_id integer NOT NULL,
    team character varying(3) NOT NULL,
    score integer NOT NULL,
    passing_yards integer NOT NULL,
    sacks integer NOT NULL,
    punts integer NOT NULL,
    return_yards integer NOT NULL
);


ALTER TABLE football.stat OWNER TO postgres;

--
-- Data for Name: game; Type: TABLE DATA; Schema: football; Owner: postgres
--

COPY football.game (id, week, home_team, away_team, state, quarter, quarter_seconds_remaining) FROM stdin;
2019090500	1	CHI	GB	\N	\N	\N
\.


--
-- Data for Name: play; Type: TABLE DATA; Schema: football; Owner: postgres
--

COPY football.play (id, play_id, game_id, week, home_team, home_score, away_team, away_score, quarter, quarter_seconds_remaining, half_seconds_remaining, game_seconds_remaining, game_date, game_half, description, play_time, side_of_field, yard_line, play_type, down, goal_to_go, yardline_100, drive, yards_to_go, quarter_end, touchdown, shotgun, no_huddle, qb_dropback, qb_kneel, qb_spike, qb_scramble, qb_hit, first_down_pass, first_down_rush, first_down_penalty, third_down_converted, third_down_failed, fourth_down_converted, fourth_down_failed, pass_attempt, pass_touchdown, complete_pass, incomplete_pass, interception, rush_attempt, rush_touchdown, fumble, fumble_forced, fumble_not_forced, fumble_lost, fumble_out_of_bounds, touchback, return_touchdown, punt_attempt, punt_blocked, kickoff_attempt, own_kickoff_recovery, own_kickoff_recovery_td, extra_point_attempt, two_point_attempt, field_goal_attempt, solo_tackle, assist_tackle, tackle_for_loss, sack, safety, penalty, replay_or_challenge, defensive_two_point_attempt, defensive_two_point_conv, defensive_extra_point_attempt, defensive_extra_point_conv, posteam, posteam_type, posteam_score, defteam, defteam_score, td_team, net_yards, yards_gained, pass_length, pass_location, air_yards, yards_after_catch, run_location, run_gap, field_goal_result, extra_point_result, two_point_conv_result, return_team, return_yards, kick_distance, penalty_team, penalty_type, penalty_yards, replay_or_challenge_result, timeout, timeout_team, home_timeouts_remaining, away_timeouts_remaining, posteam_timeouts_remaining, defteam_timeouts_remaining) FROM stdin;
1	35	2019090500	1	CHI	0	GB	0	1	900	1800	3600	2019-09-05	Half1	E.Pineiro kicks 65 yards from CHI 35 to end zone, Touchback.	15:00	CHI	CHI 35	kickoff	\N	0	35	1	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	\N	CHI	\N	\N	-10	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
2	50	2019090500	1	CHI	0	GB	0	1	900	1800	3600	2019-09-05	Half1	(15:00) A.Jones left tackle to GB 25 for no gain (R.Smith).	15:00	GB	GB 25	run	1	0	75	1	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	0	\N	-10	0	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
3	71	2019090500	1	CHI	0	GB	0	1	873	1773	3573	2019-09-05	Half1	(14:33) A.Rodgers pass short left to A.Jones to GB 25 for no gain (R.Smith).	14:33	GB	GB 25	pass	2	0	75	1	10	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	0	\N	-10	0	short	left	-1	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
4	95	2019090500	1	CHI	0	GB	0	1	825	1725	3525	2019-09-05	Half1	(13:45) (Shotgun) A.Rodgers sacked at GB 15 for -10 yards (R.Robertson-Harris). Penalty on GB-L.Taylor, Offensive Holding, declined.	13:45	GB	GB 25	pass	3	0	75	1	10	f	f	t	f	t	f	f	f	t	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	GB	away	0	CHI	0	\N	-10	-10	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
5	125	2019090500	1	CHI	0	GB	0	1	795	1695	3495	2019-09-05	Half1	(13:15) (Punt formation) J.Scott punts 53 yards to CHI 32, Center-H.Bradley. T.Cohen pushed ob at CHI 43 for 11 yards (A.Amos).	13:15	GB	GB 15	punt	4	0	85	1	20	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	0	\N	-10	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	CHI	11	53	\N	\N	\N	\N	f	\N	3	3	3	3
6	155	2019090500	1	CHI	0	GB	0	1	781	1681	3481	2019-09-05	Half1	(13:01) (Shotgun) T.Cohen FUMBLES (Aborted) at CHI 37, and recovers at CHI 39. PENALTY on GB-K.Clark, Defensive Holding, 5 yards, enforced at CHI 43 - No Play.	13:01	CHI	CHI 43	no_play	1	0	57	2	10	f	f	t	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	0	GB	0	\N	12	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	GB	Defensive Holding	5	\N	f	\N	3	3	3	3
7	195	2019090500	1	CHI	0	GB	0	1	753	1653	3453	2019-09-05	Half1	(12:33) (Shotgun) M.Davis left guard to GB 47 for 5 yards (K.Clark; D.Lowry).	12:33	CHI	CHI 48	run	1	0	52	2	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	12	5	\N	\N	\N	\N	left	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
8	216	2019090500	1	CHI	0	GB	0	1	718	1618	3418	2019-09-05	Half1	(11:58) (Shotgun) M.Trubisky pass incomplete short left to A.Robinson (P.Smith).	11:58	GB	GB 47	pass	2	0	47	2	5	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	12	0	short	left	2	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
9	238	2019090500	1	CHI	0	GB	0	1	714	1614	3414	2019-09-05	Half1	(11:54) (Shotgun) M.Trubisky scrambles right tackle to GB 40 for 7 yards (R.Greene).	11:54	GB	GB 47	run	3	0	47	2	5	f	f	t	f	t	f	f	t	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	12	7	\N	\N	\N	\N	right	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
10	260	2019090500	1	CHI	0	GB	0	1	672	1572	3372	2019-09-05	Half1	(11:12) (Shotgun) M.Trubisky pass incomplete short left to T.Cohen.	11:12	GB	GB 40	pass	1	0	40	2	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	12	0	short	left	15	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
11	282	2019090500	1	CHI	0	GB	0	1	667	1567	3367	2019-09-05	Half1	(11:07) (Shotgun) M.Trubisky right guard to GB 39 for 1 yard (A.Amos).	11:07	GB	GB 40	run	2	0	40	2	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	12	1	\N	\N	\N	\N	right	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
12	303	2019090500	1	CHI	0	GB	0	1	625	1525	3325	2019-09-05	Half1	(10:25) (Shotgun) M.Trubisky sacked at GB 45 for -6 yards (B.Martinez).	10:25	GB	GB 39	pass	3	0	39	2	9	f	f	t	f	t	f	f	f	t	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	12	-6	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
13	327	2019090500	1	CHI	0	GB	0	1	587	1487	3287	2019-09-05	Half1	(9:47) (Punt formation) P.O'Donnell punts 33 yards to GB 12, Center-P.Scales, fair catch by T.Davis.	09:47	GB	GB 45	punt	4	0	45	2	15	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	12	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	33	\N	\N	\N	\N	f	\N	3	3	3	3
14	381	2019090500	1	CHI	0	GB	0	1	579	1479	3279	2019-09-05	Half1	(9:39) (Shotgun) A.Rodgers pass incomplete short right to J.Graham.	09:39	GB	GB 12	pass	1	0	88	3	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	0	\N	-7	0	short	right	3	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
15	403	2019090500	1	CHI	0	GB	0	1	573	1473	3273	2019-09-05	Half1	(9:33) A.Jones right tackle to GB 12 for no gain (K.Mack). FUMBLES (K.Mack), and recovers at GB 12.	09:33	GB	GB 12	run	2	0	88	3	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	t	t	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	0	\N	-7	0	\N	\N	\N	\N	right	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
16	424	2019090500	1	CHI	0	GB	0	1	531	1431	3231	2019-09-05	Half1	(8:51) (Shotgun) A.Rodgers sacked at GB 5 for -7 yards (L.Floyd).	08:51	GB	GB 12	pass	3	0	88	3	10	f	f	t	f	t	f	f	f	t	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	GB	away	0	CHI	0	\N	-7	-7	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
17	443	2019090500	1	CHI	0	GB	0	1	484	1384	3184	2019-09-05	Half1	(8:04) (Punt formation) J.Scott punts 42 yards to GB 47, Center-H.Bradley. T.Cohen to GB 41 for 6 yards (T.Summers; K.Fackrell). PENALTY on GB-T.Brown, Player Out of Bounds on Kick, 5 yards, enforced at GB 41.	08:04	GB	GB 5	punt	4	0	95	3	17	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	t	f	f	f	t	f	f	f	f	f	GB	away	0	CHI	0	\N	-7	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	CHI	6	42	GB	Player Out of Bounds on Kick	5	\N	f	\N	3	3	3	3
18	485	2019090500	1	CHI	0	GB	0	1	473	1373	3173	2019-09-05	Half1	(7:53) (Shotgun) D.Montgomery left guard to GB 32 for 4 yards (K.King).	07:53	GB	GB 36	run	1	0	36	4	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	16	4	\N	\N	\N	\N	left	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
19	518	2019090500	1	CHI	0	GB	0	1	432	1332	3132	2019-09-05	Half1	(7:12) (Shotgun) D.Montgomery left guard to GB 26 for 6 yards (Z.Smith).	07:12	GB	GB 32	run	2	0	32	4	6	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	16	6	\N	\N	\N	\N	left	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
20	539	2019090500	1	CHI	0	GB	0	1	368	1268	3068	2019-09-05	Half1	(6:08) (Shotgun) D.Montgomery left guard to GB 25 for 1 yard (K.Clark; P.Smith).	06:08	GB	GB 26	run	1	0	26	4	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	16	1	\N	\N	\N	\N	left	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
21	575	2019090500	1	CHI	0	GB	0	1	323	1223	3023	2019-09-05	Half1	(5:23) M.Davis left tackle to GB 21 for 4 yards (D.Lowry).	05:23	GB	GB 25	run	2	0	25	4	9	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	16	4	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
22	596	2019090500	1	CHI	0	GB	0	1	281	1181	2981	2019-09-05	Half1	(4:41) (Shotgun) M.Trubisky pass short right to T.Cohen pushed ob at GB 20 for 1 yard (J.Alexander).	04:41	GB	GB 21	pass	3	0	21	4	5	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	16	1	short	right	0	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
23	625	2019090500	1	CHI	3	GB	0	1	254	1154	2954	2019-09-05	Half1	(4:14) E.Pineiro 38 yard field goal is GOOD, Center-P.Scales, Holder-P.O'Donnell.	04:14	GB	GB 20	field_goal	4	0	20	4	4	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	CHI	home	0	GB	0	\N	16	0	\N	\N	\N	\N	\N	\N	made	\N	\N	\N	0	38	\N	\N	\N	\N	f	\N	3	3	3	3
24	659	2019090500	1	CHI	3	GB	0	1	249	1149	2949	2019-09-05	Half1	E.Pineiro kicks 59 yards from CHI 35 to GB 6, out of bounds.	04:09	CHI	CHI 35	kickoff	\N	0	35	5	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	3	\N	5	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	25	\N	\N	\N	\N	f	\N	3	3	3	3
25	676	2019090500	1	CHI	3	GB	0	1	249	1149	2949	2019-09-05	Half1	(4:09) A.Rodgers pass incomplete short middle to D.Adams (P.Amukamara).	04:09	GB	GB 40	pass	1	0	60	5	10	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	3	\N	5	0	short	middle	5	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
26	698	2019090500	1	CHI	3	GB	0	1	247	1147	2947	2019-09-05	Half1	(4:07) (No Huddle, Shotgun) A.Rodgers pass short right to D.Adams to GB 45 for 5 yards (K.Fuller).	04:07	GB	GB 40	pass	2	0	60	5	10	f	f	t	t	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	3	\N	5	5	short	right	5	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
27	722	2019090500	1	CHI	3	GB	0	1	205	1105	2905	2019-09-05	Half1	(3:25) (Shotgun) A.Rodgers pass incomplete short left to D.Adams.	03:25	GB	GB 45	pass	3	0	55	5	5	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	3	\N	5	0	short	left	4	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
28	744	2019090500	1	CHI	3	GB	0	1	200	1100	2900	2019-09-05	Half1	(3:20) (Punt formation) J.Scott punts 47 yards to CHI 8, Center-H.Bradley, fair catch by T.Cohen. PENALTY on CHI-J.Iyiegbuniwe, Offensive Holding, 4 yards, enforced at CHI 8.	03:20	GB	GB 45	punt	4	0	55	5	5	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	GB	away	0	CHI	3	\N	5	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	47	CHI	Offensive Holding	4	\N	f	\N	3	3	3	3
29	801	2019090500	1	CHI	3	GB	0	1	193	1093	2893	2019-09-05	Half1	(3:13) (Shotgun) M.Davis right guard to CHI 5 for 1 yard (Z.Smith).	03:13	CHI	CHI 4	run	1	0	96	6	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	1	\N	\N	\N	\N	right	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
30	822	2019090500	1	CHI	3	GB	0	1	152	1052	2852	2019-09-05	Half1	(2:32) (Shotgun) M.Trubisky pass short middle to A.Robinson to CHI 10 for 5 yards (R.Greene; J.Alexander).	02:32	CHI	CHI 5	pass	2	0	95	6	9	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	5	short	middle	3	2	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
31	846	2019090500	1	CHI	3	GB	0	1	110	1010	2810	2019-09-05	Half1	Timeout #1 by CHI at 01:50.	01:50	CHI	CHI 5	no_play	\N	0	95	6	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	t	CHI	2	3	2	3
32	863	2019090500	1	CHI	3	GB	0	1	110	1010	2810	2019-09-05	Half1	(1:50) (Shotgun) M.Trubisky pass incomplete short left to C.Patterson. PENALTY on GB-T.Williams, Defensive Pass Interference, 6 yards, enforced at CHI 10 - No Play.	01:50	CHI	CHI 10	no_play	3	0	90	6	4	f	f	t	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	0	\N	25	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	GB	Defensive Pass Interference	6	\N	f	\N	2	3	2	3
33	896	2019090500	1	CHI	3	GB	0	1	106	1006	2806	2019-09-05	Half1	(1:46) (Shotgun) M.Trubisky pass short middle to A.Shaheen to CHI 22 for 6 yards (T.Brown).	01:46	CHI	CHI 16	pass	1	0	84	6	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	6	short	middle	5	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
34	920	2019090500	1	CHI	3	GB	0	1	65	965	2765	2019-09-05	Half1	(1:05) (Shotgun) D.Montgomery left tackle to CHI 28 for 6 yards (T.Brown; K.Fackrell).	01:05	CHI	CHI 22	run	2	0	78	6	4	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	6	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
35	941	2019090500	1	CHI	3	GB	0	1	22	922	2722	2019-09-05	Half1	(:22) (Shotgun) M.Trubisky pass incomplete short middle to T.Gabriel (J.Alexander).	00:22	CHI	CHI 28	pass	1	0	72	6	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	0	short	middle	8	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
36	963	2019090500	1	CHI	3	GB	0	1	16	916	2716	2019-09-05	Half1	(:16) (Shotgun) D.Montgomery right guard to CHI 29 for 1 yard (B.Martinez).	00:16	CHI	CHI 28	run	2	0	72	6	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	1	\N	\N	\N	\N	right	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
37	984	2019090500	1	CHI	3	GB	0	1	0	900	2700	2019-09-05	Half1	END QUARTER 1	00:00	CHI	CHI 28	\N	\N	\N	\N	6	0	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	NA	NA	\N	NA	\N	\N	25	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	\N	\N
38	1001	2019090500	1	CHI	3	GB	0	2	900	900	2700	2019-09-05	Half1	(15:00) (Shotgun) M.Trubisky pass incomplete deep left to C.Patterson (K.King).	15:00	CHI	CHI 29	pass	3	0	71	6	9	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	0	deep	left	17	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
39	1023	2019090500	1	CHI	3	GB	0	2	894	894	2694	2019-09-05	Half1	(14:54) (Punt formation) P.O'Donnell punts 46 yards to GB 25, Center-P.Scales. T.Davis to GB 26 for 1 yard (K.Pierre-Louis).	14:54	CHI	CHI 29	punt	4	0	71	6	9	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	0	\N	25	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	GB	1	46	\N	\N	\N	\N	f	\N	2	3	2	3
40	1082	2019090500	1	CHI	3	GB	0	2	883	883	2683	2019-09-05	Half1	(14:43) A.Rodgers pass deep middle to M.Valdes-Scantling to CHI 27 for 47 yards (P.Amukamara).	14:43	GB	GB 26	pass	1	0	74	7	10	f	f	f	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	3	\N	74	47	deep	middle	47	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
41	1106	2019090500	1	CHI	3	GB	0	2	848	848	2648	2019-09-05	Half1	(14:08) A.Rodgers pass short left to M.Lewis pushed ob at CHI 18 for 9 yards (L.Floyd).	14:08	CHI	CHI 27	pass	1	0	27	7	10	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	3	\N	74	9	short	left	1	8	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
42	1135	2019090500	1	CHI	3	GB	0	2	824	824	2624	2019-09-05	Half1	(13:44) A.Rodgers pass short right to D.Adams to CHI 8 for 10 yards (E.Jackson).	13:44	CHI	CHI 18	pass	2	0	18	7	1	f	f	f	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	3	\N	74	10	short	right	3	7	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
43	1159	2019090500	1	CHI	3	GB	6	2	795	795	2595	2019-09-05	Half1	(13:15) A.Rodgers pass short right to J.Graham for 8 yards, TOUCHDOWN. Penalty on CHI, Defensive Too Many Men on Field, declined.	13:15	CHI	CHI 8	pass	1	1	8	7	8	f	t	f	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	0	CHI	3	GB	74	8	short	right	8	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
44	1184	2019090500	1	CHI	3	GB	7	2	788	788	2588	2019-09-05	Half1	M.Crosby extra point is GOOD, Center-H.Bradley, Holder-J.Scott.	13:08	CHI	CHI 15	extra_point	\N	0	15	7	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	6	CHI	3	\N	74	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	33	\N	\N	\N	\N	f	\N	2	3	3	2
45	1227	2019090500	1	CHI	3	GB	7	2	788	788	2588	2019-09-05	Half1	M.Crosby kicks 65 yards from GB 35 to end zone, Touchback.	13:08	GB	GB 35	kickoff	\N	0	35	8	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	34	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
46	1248	2019090500	1	CHI	3	GB	7	2	788	788	2588	2019-09-05	Half1	(13:08) (Shotgun) M.Trubisky pass deep left to A.Robinson to GB 48 for 27 yards (T.Brown).	13:08	CHI	CHI 25	pass	1	0	75	8	10	f	f	t	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	34	27	deep	left	25	2	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
47	1272	2019090500	1	CHI	3	GB	7	2	744	744	2544	2019-09-05	Half1	(12:24) (Shotgun) M.Trubisky pass incomplete short middle to A.Robinson (T.Williams).	12:24	GB	GB 48	pass	1	0	48	8	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	34	0	short	middle	3	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
48	1294	2019090500	1	CHI	3	GB	7	2	739	739	2539	2019-09-05	Half1	(12:19) (Shotgun) M.Trubisky pass short middle to T.Cohen to GB 39 for 9 yards (D.Savage).	12:19	GB	GB 48	pass	2	0	48	8	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	34	9	short	middle	6	3	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
49	1318	2019090500	1	CHI	3	GB	7	2	696	696	2496	2019-09-05	Half1	(11:36) C.Patterson right guard to GB 41 for -2 yards (A.Amos; K.Clark). Penalty on CHI-J.Daniels, Offensive Holding, declined.	11:36	GB	GB 39	run	3	0	39	8	1	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	t	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	34	-2	\N	\N	\N	\N	right	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
50	1350	2019090500	1	CHI	3	GB	7	2	680	680	2480	2019-09-05	Half1	(11:20) (Punt formation) P.O'Donnell punts 37 yards to GB 4, Center-P.Scales, downed by CHI-J.Iyiegbuniwe.	11:20	GB	GB 41	punt	4	0	41	8	3	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	34	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	37	\N	\N	\N	\N	f	\N	2	3	2	3
51	1404	2019090500	1	CHI	3	GB	7	2	670	670	2470	2019-09-05	Half1	(11:10) A.Rodgers pass short right to M.Valdes-Scantling to GB 13 for 9 yards (K.Fuller).	11:10	GB	GB 4	pass	1	0	96	9	10	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	9	short	right	8	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
52	1429	2019090500	1	CHI	3	GB	7	2	630	630	2430	2019-09-05	Half1	(10:30) A.Rodgers pass short right to M.Valdes-Scantling to GB 14 for 1 yard (K.Fuller).	10:30	GB	GB 13	pass	2	0	87	9	1	f	f	f	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	1	short	right	0	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
53	1453	2019090500	1	CHI	3	GB	7	2	586	586	2386	2019-09-05	Half1	(9:46) J.Williams right tackle to GB 19 for 5 yards (D.Trevathan).	09:46	GB	GB 14	run	1	0	86	9	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	5	\N	\N	\N	\N	right	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
54	1474	2019090500	1	CHI	3	GB	7	2	546	546	2346	2019-09-05	Half1	(9:06) M.Valdes-Scantling left end to GB 19 for no gain (R.Robertson-Harris).	09:06	GB	GB 19	run	2	0	81	9	5	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	0	\N	\N	\N	\N	left	end	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
55	1495	2019090500	1	CHI	3	GB	7	2	500	500	2300	2019-09-05	Half1	(8:20) (Shotgun) A.Rodgers pass short left to D.Adams pushed ob at GB 30 for 11 yards (P.Amukamara).	08:20	GB	GB 19	pass	3	0	81	9	5	f	f	t	f	t	f	f	f	f	t	f	f	t	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	11	short	left	3	8	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
56	1524	2019090500	1	CHI	3	GB	7	2	462	462	2262	2019-09-05	Half1	(7:42) (Shotgun) A.Rodgers pass short left to J.Williams to GB 35 for 5 yards (B.Nichols; R.Smith).	07:42	GB	GB 30	pass	1	0	70	9	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	5	short	left	-3	8	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
57	1548	2019090500	1	CHI	3	GB	7	2	423	423	2223	2019-09-05	Half1	(7:03) (Shotgun) J.Williams left tackle to GB 34 for -1 yards (R.Robertson-Harris).	07:03	GB	GB 35	run	2	0	65	9	5	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	-1	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
58	1569	2019090500	1	CHI	3	GB	7	2	379	379	2179	2019-09-05	Half1	(6:19) (Shotgun) A.Rodgers sacked at GB 26 for -8 yards (A.Hicks).	06:19	GB	GB 34	pass	3	0	66	9	6	f	f	t	f	t	f	f	f	t	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	-8	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
59	1588	2019090500	1	CHI	3	GB	7	2	337	337	2137	2019-09-05	Half1	(5:37) (Punt formation) J.Scott punts 63 yards to CHI 11, Center-H.Bradley, downed by GB-W.Redmond.	05:37	GB	GB 26	punt	4	0	74	9	14	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	22	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	63	\N	\N	\N	\N	f	\N	2	3	3	2
60	1633	2019090500	1	CHI	3	GB	7	2	325	325	2125	2019-09-05	Half1	(5:25) (Shotgun) M.Trubisky pass short left to A.Robinson pushed ob at CHI 20 for 9 yards (K.King).	05:25	CHI	CHI 11	pass	1	0	89	10	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	12	9	short	left	6	3	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
61	1662	2019090500	1	CHI	3	GB	7	2	300	300	2100	2019-09-05	Half1	(5:00) (Shotgun) M.Trubisky pass short middle to M.Davis to CHI 27 for 7 yards (K.Clark).	05:00	CHI	CHI 20	pass	2	0	80	10	1	f	f	t	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	12	7	short	middle	-4	11	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
62	1686	2019090500	1	CHI	3	GB	7	2	259	259	2059	2019-09-05	Half1	(4:19) (Shotgun) T.Cohen left tackle pushed ob at CHI 29 for 2 yards (D.Savage). PENALTY on CHI-K.Long, Offensive Holding, 10 yards, enforced at CHI 27 - No Play.	04:19	CHI	CHI 27	no_play	1	0	73	10	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	7	\N	12	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	CHI	Offensive Holding	10	\N	f	\N	2	3	2	3
63	1723	2019090500	1	CHI	3	GB	7	2	226	226	2026	2019-09-05	Half1	(3:46) (Shotgun) M.Trubisky sacked at CHI 16 for -1 yards (Z.Smith).	03:46	CHI	CHI 17	pass	1	0	83	10	20	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	12	-1	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
64	1751	2019090500	1	CHI	3	GB	7	2	180	180	1980	2019-09-05	Half1	(3:00) (Shotgun) M.Trubisky pass short right to T.Cohen to CHI 23 for 7 yards (D.Savage).	03:00	CHI	CHI 16	pass	2	0	84	10	21	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	12	7	short	right	6	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
65	1775	2019090500	1	CHI	3	GB	7	2	139	139	1939	2019-09-05	Half1	(2:19) (Shotgun) M.Trubisky pass incomplete short middle to A.Robinson (D.Savage).	02:19	CHI	CHI 23	pass	3	0	77	10	14	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	12	0	short	middle	13	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
66	1797	2019090500	1	CHI	3	GB	7	2	134	134	1934	2019-09-05	Half1	(2:14) (Punt formation) P.O'Donnell punts 42 yards to GB 35, Center-P.Scales, fair catch by T.Davis.	02:14	CHI	CHI 23	punt	4	0	77	10	14	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	12	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	42	\N	\N	\N	\N	f	\N	2	3	2	3
67	1825	2019090500	1	CHI	3	GB	7	2	127	127	1927	2019-09-05	Half1	(2:07) (Shotgun) A.Jones left tackle to GB 38 for 3 yards (P.Amukamara; D.Trevathan).	02:07	GB	GB 35	run	1	0	65	11	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	14	3	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
68	1846	2019090500	1	CHI	3	GB	7	2	120	120	1920	2019-09-05	Half1	Two-Minute Warning	02:00	GB	GB 35	\N	\N	\N	\N	11	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	NA	NA	\N	NA	\N	\N	14	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	\N	\N
69	1863	2019090500	1	CHI	3	GB	7	2	120	120	1920	2019-09-05	Half1	(2:00) (Shotgun) A.Rodgers pass incomplete deep right to D.Adams (B.Skrine).	02:00	GB	GB 38	pass	2	0	62	11	7	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	14	0	deep	right	34	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
70	1885	2019090500	1	CHI	3	GB	7	2	114	114	1914	2019-09-05	Half1	(1:54) (Shotgun) A.Rodgers pass short left to J.Williams to GB 48 for 10 yards (R.Smith).	01:54	GB	GB 38	pass	3	0	62	11	7	f	f	t	f	t	f	f	f	f	t	f	f	t	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	14	10	short	left	-3	13	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
71	1909	2019090500	1	CHI	3	GB	7	2	87	87	1887	2019-09-05	Half1	(1:27) (No Huddle, Shotgun) A.Rodgers pass short right to J.Graham to CHI 46 for 6 yards (B.Skrine).	01:27	GB	GB 48	pass	1	0	52	11	10	f	f	t	t	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	14	6	short	right	-2	8	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
72	1937	2019090500	1	CHI	3	GB	7	2	64	64	1864	2019-09-05	Half1	(1:04) (Shotgun) A.Rodgers pass incomplete short right to D.Adams (K.Fuller).	01:04	CHI	CHI 46	pass	2	0	46	11	4	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	14	0	short	right	3	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
73	1959	2019090500	1	CHI	3	GB	7	2	60	60	1860	2019-09-05	Half1	(1:00) (Shotgun) A.Rodgers pass incomplete deep left to M.Valdes-Scantling.	01:00	CHI	CHI 46	pass	3	0	46	11	4	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	14	0	deep	left	28	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	3	2
74	1981	2019090500	1	CHI	3	GB	7	2	55	55	1855	2019-09-05	Half1	(:55) (Shotgun) PENALTY on GB-A.Rodgers, False Start, 5 yards, enforced at CHI 46 - No Play.	00:55	CHI	CHI 46	no_play	4	0	46	11	4	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	GB	away	7	CHI	3	\N	14	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	GB	False Start	5	\N	f	\N	2	3	3	2
75	2004	2019090500	1	CHI	3	GB	7	2	55	55	1855	2019-09-05	Half1	(:55) (Punt formation) J.Scott punts 36 yards to CHI 15, Center-H.Bradley, downed by GB-H.Bradley.	00:55	GB	GB 49	punt	4	0	51	11	9	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	14	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	36	\N	\N	\N	\N	f	\N	2	3	3	2
76	2049	2019090500	1	CHI	3	GB	7	2	43	43	1843	2019-09-05	Half1	(:43) (Shotgun) M.Trubisky pass short left to C.Patterson ran ob at CHI 18 for 3 yards (K.King).	00:43	CHI	CHI 15	pass	1	0	85	12	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	0	3	short	left	-2	5	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
77	2078	2019090500	1	CHI	3	GB	7	2	38	38	1838	2019-09-05	Half1	(:38) (Shotgun) M.Trubisky sacked at CHI 16 for -2 yards (K.King).	00:38	CHI	CHI 18	pass	2	0	82	12	7	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	0	-2	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
78	2106	2019090500	1	CHI	3	GB	7	2	31	31	1831	2019-09-05	Half1	(:31) (Shotgun) M.Trubisky pass short right to M.Davis to CHI 15 for -1 yards (B.Martinez; J.Alexander).	00:31	CHI	CHI 16	pass	3	0	84	12	9	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	0	-1	short	right	-4	3	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	3	2	3
79	2134	2019090500	1	CHI	3	GB	7	2	24	24	1824	2019-09-05	Half1	Timeout #2 by GB at 00:24.	00:24	CHI	CHI 16	no_play	\N	0	84	12	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	0	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	t	GB	2	2	2	2
80	2151	2019090500	1	CHI	3	GB	7	2	24	24	1824	2019-09-05	Half1	(:24) (Punt formation) P.O'Donnell punts 52 yards to GB 33, Center-P.Scales, downed by CHI-C.Patterson.	00:24	CHI	CHI 15	punt	4	0	85	12	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	0	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	52	\N	\N	\N	\N	f	\N	2	2	2	2
81	2179	2019090500	1	CHI	3	GB	7	2	15	15	1815	2019-09-05	Half1	(:15) (Shotgun) A.Rodgers pass short middle to J.Graham to GB 49 for 16 yards (H.Clinton-Dix).	00:15	GB	GB 33	pass	1	0	67	13	10	f	f	t	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	16	16	short	middle	7	9	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	2	2	2
82	2203	2019090500	1	CHI	3	GB	7	2	9	9	1809	2019-09-05	Half1	Timeout #3 by GB at 00:09.	00:09	GB	GB 33	no_play	\N	0	67	13	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	16	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	t	GB	2	1	1	2
83	2220	2019090500	1	CHI	3	GB	7	2	9	9	1809	2019-09-05	Half1	(:09) (Shotgun) A.Rodgers pass incomplete deep right (K.Fuller).	00:09	GB	GB 49	pass	1	0	51	13	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	16	0	deep	right	51	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	1	1	2
84	2242	2019090500	1	CHI	3	GB	7	2	0	0	1800	2019-09-05	Half1	END QUARTER 2	00:00	GB	GB 49	\N	\N	\N	\N	13	0	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	NA	NA	\N	NA	\N	\N	16	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	1	\N	\N
85	2260	2019090500	1	CHI	3	GB	7	3	900	1800	1800	2019-09-05	Half2	M.Crosby kicks 65 yards from GB 35 to end zone, Touchback.	15:00	GB	GB 35	kickoff	\N	0	35	14	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	2	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
86	2284	2019090500	1	CHI	3	GB	7	3	900	1800	1800	2019-09-05	Half2	(15:00) D.Montgomery left tackle to CHI 25 for no gain (T.Brown; P.Smith).	15:00	CHI	CHI 25	run	1	0	75	14	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	2	0	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
87	2305	2019090500	1	CHI	3	GB	7	3	861	1761	1761	2019-09-05	Half2	(14:21) (Shotgun) M.Trubisky pass incomplete short middle to J.Wims [D.Lowry].	14:21	CHI	CHI 25	pass	2	0	75	14	10	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	2	0	short	middle	15	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
88	2327	2019090500	1	CHI	3	GB	7	3	855	1755	1755	2019-09-05	Half2	(14:15) (Shotgun) M.Trubisky pass short right to M.Davis pushed ob at CHI 27 for 2 yards (T.Williams).	14:15	CHI	CHI 25	pass	3	0	75	14	10	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	2	2	short	right	1	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
89	2356	2019090500	1	CHI	3	GB	7	3	830	1730	1730	2019-09-05	Half2	(13:50) (Punt formation) P.O'Donnell punts 39 yards to GB 34, Center-P.Scales, fair catch by T.Davis.	13:50	CHI	CHI 27	punt	4	0	73	14	8	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	2	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	39	\N	\N	\N	\N	f	\N	3	3	3	3
90	2401	2019090500	1	CHI	3	GB	7	3	822	1722	1722	2019-09-05	Half2	(13:42) A.Jones right end to GB 37 for 3 yards (D.Trevathan).	13:42	GB	GB 34	run	1	0	66	15	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	19	3	\N	\N	\N	\N	right	end	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
91	2422	2019090500	1	CHI	3	GB	7	3	797	1697	1697	2019-09-05	Half2	(13:17) (No Huddle) A.Rodgers pass incomplete deep left to D.Adams. PENALTY on GB-D.Bakhtiari, Offensive Holding, 10 yards, enforced at GB 37 - No Play.	13:17	GB	GB 37	no_play	2	0	63	15	7	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	GB	away	7	CHI	3	\N	19	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	GB	Offensive Holding	10	\N	f	\N	3	3	3	3
92	2455	2019090500	1	CHI	3	GB	7	3	791	1691	1691	2019-09-05	Half2	(13:11) (Shotgun) A.Rodgers pass incomplete deep middle to J.Graham. PENALTY on CHI-R.Smith, Defensive Pass Interference, 38 yards, enforced at GB 27 - No Play.	13:11	GB	GB 27	no_play	2	0	73	15	17	f	f	t	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	GB	away	7	CHI	3	\N	19	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	CHI	Defensive Pass Interference	38	\N	f	\N	3	3	3	3
93	2488	2019090500	1	CHI	3	GB	7	3	784	1684	1684	2019-09-05	Half2	(13:04) A.Rodgers pass incomplete short left [R.Robertson-Harris].	13:04	CHI	CHI 35	pass	1	0	35	15	10	f	f	f	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	19	0	short	left	-3	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	3	3	3
94	2517	2019090500	1	CHI	3	GB	7	3	779	1679	1679	2019-09-05	Half2	Timeout #1 by GB at 12:59.	12:59	CHI	CHI 35	no_play	\N	0	35	15	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	19	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	t	GB	3	2	2	3
95	2534	2019090500	1	CHI	3	GB	7	3	779	1679	1679	2019-09-05	Half2	(12:59) (Shotgun) A.Rodgers pass short middle to M.Lewis to CHI 30 for 5 yards (D.Trevathan).	12:59	CHI	CHI 35	pass	2	0	35	15	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	19	5	short	middle	5	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
96	2558	2019090500	1	CHI	3	GB	7	3	736	1636	1636	2019-09-05	Half2	(12:16) (Shotgun) A.Rodgers scrambles up the middle to CHI 25 for 5 yards (H.Clinton-Dix). FUMBLES (H.Clinton-Dix), and recovers at CHI 22. A.Rodgers to CHI 22 for no gain (H.Clinton-Dix). PENALTY on GB-D.Bakhtiari, Offensive Holding, 10 yards, enforced at CHI 30 - No Play.	12:16	CHI	CHI 30	no_play	3	0	30	15	5	f	f	t	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	GB	away	7	CHI	3	\N	19	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	GB	Offensive Holding	10	\N	f	\N	3	2	2	3
97	2606	2019090500	1	CHI	3	GB	7	3	703	1603	1603	2019-09-05	Half2	(11:43) (Shotgun) A.Rodgers sacked at CHI 47 for -7 yards (L.Floyd).	11:43	CHI	CHI 40	pass	3	0	40	15	15	f	f	t	f	t	f	f	f	t	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	19	-7	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
98	2625	2019090500	1	CHI	3	GB	7	3	658	1558	1558	2019-09-05	Half2	(10:58) (Punt formation) J.Scott punts 37 yards to CHI 10, Center-H.Bradley, fair catch by T.Cohen.	10:58	CHI	CHI 47	punt	4	0	47	15	22	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	19	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	37	\N	\N	\N	\N	f	\N	3	2	2	3
99	2670	2019090500	1	CHI	3	GB	7	3	651	1551	1551	2019-09-05	Half2	(10:51) (Shotgun) M.Davis left guard to CHI 18 for 8 yards (D.Lowry; B.Martinez).	10:51	CHI	CHI 10	run	1	0	90	16	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	9	8	\N	\N	\N	\N	left	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
100	2691	2019090500	1	CHI	3	GB	7	3	631	1531	1531	2019-09-05	Half2	(10:31) (No Huddle, Shotgun) M.Davis left guard to CHI 19 for 1 yard (P.Smith).	10:31	CHI	CHI 18	run	2	0	82	16	2	f	f	t	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	9	1	\N	\N	\N	\N	left	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
101	2712	2019090500	1	CHI	3	GB	7	3	587	1487	1487	2019-09-05	Half2	(9:47) (Shotgun) M.Trubisky sacked at CHI 19 for 0 yards (sack split by P.Smith and K.Clark).	09:47	CHI	CHI 19	pass	3	0	81	16	1	f	f	t	f	t	f	f	f	t	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	9	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
102	2731	2019090500	1	CHI	3	GB	7	3	550	1450	1450	2019-09-05	Half2	(9:10) (Punt formation) P.O'Donnell punts 39 yards to GB 42, Center-P.Scales, out of bounds.	09:10	CHI	CHI 19	punt	4	0	81	16	1	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	9	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	39	\N	\N	\N	\N	f	\N	3	2	3	2
103	2787	2019090500	1	CHI	3	GB	7	3	541	1441	1441	2019-09-05	Half2	(9:01) (Shotgun) A.Jones right guard to GB 42 for no gain (K.Mack).	09:01	GB	GB 42	run	1	0	58	17	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	-10	0	\N	\N	\N	\N	right	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
104	2808	2019090500	1	CHI	3	GB	7	3	496	1396	1396	2019-09-05	Half2	(8:16) (Shotgun) A.Rodgers sacked at GB 37 for -5 yards (A.Lynch).	08:16	GB	GB 42	pass	2	0	58	17	10	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	-10	-5	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
105	2827	2019090500	1	CHI	3	GB	7	3	447	1347	1347	2019-09-05	Half2	(7:27) (Shotgun) A.Rodgers pass short left to M.Valdes-Scantling to GB 32 for -5 yards (K.Mack).	07:27	GB	GB 37	pass	3	0	63	17	15	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	-10	-5	short	left	-5	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
106	2851	2019090500	1	CHI	3	GB	7	3	403	1303	1303	2019-09-05	Half2	(6:43) (Punt formation) J.Scott punts 50 yards to CHI 18, Center-H.Bradley. T.Cohen to CHI 23 for 5 yards (J.Williams).	06:43	GB	GB 32	punt	4	0	68	17	20	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	-10	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	CHI	5	50	\N	\N	\N	\N	f	\N	3	2	2	3
107	2893	2019090500	1	CHI	3	GB	7	3	390	1290	1290	2019-09-05	Half2	(6:30) (Shotgun) M.Trubisky pass deep left to A.Robinson to CHI 45 for 22 yards (T.Brown).	06:30	CHI	CHI 23	pass	1	0	77	18	10	f	f	t	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	47	22	deep	left	21	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
108	2917	2019090500	1	CHI	3	GB	7	3	341	1241	1241	2019-09-05	Half2	(5:41) (Shotgun) PENALTY on CHI, Delay of Game, 5 yards, enforced at CHI 45 - No Play.	05:41	CHI	CHI 45	no_play	1	0	55	18	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	7	\N	47	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	CHI	Delay of Game	5	\N	f	\N	3	2	3	2
109	2940	2019090500	1	CHI	3	GB	7	3	325	1225	1225	2019-09-05	Half2	(5:25) (Shotgun) M.Trubisky pass deep left to D.Montgomery to GB 33 for 27 yards (K.King).	05:25	CHI	CHI 40	pass	1	0	60	18	15	f	f	t	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	47	27	deep	left	20	7	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
110	2964	2019090500	1	CHI	3	GB	7	3	286	1186	1186	2019-09-05	Half2	(4:46) (Shotgun) M.Trubisky pass short left to T.Cohen pushed ob at GB 28 for 5 yards (R.Greene).	04:46	GB	GB 33	pass	1	0	33	18	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	47	5	short	left	-2	7	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
111	2993	2019090500	1	CHI	3	GB	7	3	252	1152	1152	2019-09-05	Half2	(4:12) (Shotgun) M.Trubisky pass incomplete deep right to A.Robinson (T.Williams).	04:12	GB	GB 28	pass	2	0	28	18	5	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	47	0	deep	right	23	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
112	3015	2019090500	1	CHI	3	GB	7	3	245	1145	1145	2019-09-05	Half2	(4:05) (Shotgun) PENALTY on CHI, Delay of Game, 5 yards, enforced at GB 28 - No Play.	04:05	GB	GB 28	no_play	3	0	28	18	5	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	7	\N	47	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	CHI	Delay of Game	5	\N	f	\N	3	2	3	2
113	3038	2019090500	1	CHI	3	GB	7	3	245	1145	1145	2019-09-05	Half2	(4:05) (Shotgun) M.Trubisky pass incomplete short right to C.Patterson.	04:05	GB	GB 33	pass	3	0	33	18	10	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	47	0	short	right	15	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
114	3060	2019090500	1	CHI	3	GB	7	3	240	1140	1140	2019-09-05	Half2	(4:00) (Shotgun) M.Trubisky scrambles up the middle to GB 30 for 3 yards (R.Greene).	04:00	GB	GB 33	run	4	0	33	18	10	f	f	t	f	t	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	47	3	\N	\N	\N	\N	middle	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
115	3087	2019090500	1	CHI	3	GB	7	3	234	1134	1134	2019-09-05	Half2	(3:54) A.Jones left tackle to GB 39 for 9 yards (H.Clinton-Dix).	03:54	GB	GB 30	run	1	0	70	19	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	20	9	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
116	3108	2019090500	1	CHI	3	GB	7	3	198	1098	1098	2019-09-05	Half2	(3:18) A.Jones right tackle to GB 45 for 6 yards (K.Fuller).	03:18	GB	GB 39	run	2	0	61	19	1	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	20	6	\N	\N	\N	\N	right	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
117	3129	2019090500	1	CHI	3	GB	7	3	151	1051	1051	2019-09-05	Half2	(2:31) A.Jones right guard to GB 47 for 2 yards (D.Trevathan).	02:31	GB	GB 45	run	1	0	55	19	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	20	2	\N	\N	\N	\N	right	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
118	3150	2019090500	1	CHI	3	GB	7	3	118	1018	1018	2019-09-05	Half2	(1:58) (Shotgun) A.Rodgers pass short right to D.Adams ran ob at CHI 43 for 10 yards (K.Fuller).	01:58	GB	GB 47	pass	2	0	53	19	8	f	f	t	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	20	10	short	right	6	4	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
119	3179	2019090500	1	CHI	3	GB	7	3	83	983	983	2019-09-05	Half2	(1:23) (Shotgun) J.Williams left end to CHI 45 for -2 yards (E.Jackson).	01:23	CHI	CHI 43	run	1	0	43	19	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	20	-2	\N	\N	\N	\N	left	end	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
120	3200	2019090500	1	CHI	3	GB	7	3	42	942	942	2019-09-05	Half2	(:42) (Shotgun) PENALTY on GB-T.Davis, False Start, 5 yards, enforced at CHI 45 - No Play.	00:42	CHI	CHI 45	no_play	2	0	45	19	12	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	GB	away	7	CHI	3	\N	20	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	GB	False Start	5	\N	f	\N	3	2	2	3
121	3227	2019090500	1	CHI	3	GB	7	3	19	919	919	2019-09-05	Half2	(:19) (Shotgun) A.Rodgers pass incomplete short right [A.Hicks].	00:19	MID	MID 50	pass	2	0	50	19	17	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	20	0	short	right	-1	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
122	3249	2019090500	1	CHI	3	GB	7	3	12	912	912	2019-09-05	Half2	(:12) (Shotgun) A.Rodgers pass incomplete deep left to J.Graham (P.Amukamara). Penalty on GB-B.Bulaga, Offensive Holding, declined.	00:12	MID	MID 50	pass	3	0	50	19	17	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	20	0	deep	left	42	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	2	3
123	3282	2019090500	1	CHI	3	GB	7	3	0	900	900	2019-09-05	Half2	END QUARTER 3	00:00	MID	MID 50	\N	\N	0	50	19	0	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	NA	NA	\N	NA	\N	\N	20	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	\N	\N
124	3299	2019090500	1	CHI	3	GB	7	4	900	900	900	2019-09-05	Half2	(15:00) (Punt formation) J.Scott punts 37 yards to CHI 13, Center-H.Bradley, fair catch by T.Cohen.	15:00	MID	MID 50	punt	4	0	50	19	17	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	20	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	37	\N	\N	\N	\N	f	\N	3	2	2	3
125	3327	2019090500	1	CHI	3	GB	7	4	893	893	893	2019-09-05	Half2	(14:53) (Shotgun) M.Trubisky pass incomplete short left.	14:53	CHI	CHI 13	pass	1	0	87	20	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	23	0	short	left	12	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	2	3	2
126	3349	2019090500	1	CHI	3	GB	7	4	888	888	888	2019-09-05	Half2	(14:48) (Shotgun) M.Trubisky pass short middle to A.Robinson pushed ob at CHI 27 for 14 yards (T.Williams). PENALTY on GB-T.Williams, Unnecessary Roughness, 15 yards, enforced at CHI 27.	14:48	CHI	CHI 13	pass	2	0	87	20	10	f	f	t	f	t	f	f	f	f	t	f	t	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	7	\N	23	14	short	middle	4	10	\N	\N	\N	\N	\N	\N	0	\N	GB	Unnecessary Roughness	15	\N	f	\N	3	2	3	2
127	3389	2019090500	1	CHI	3	GB	7	4	865	865	865	2019-09-05	Half2	(14:25) M.Trubisky pass short right to T.Gabriel to GB 43 for 15 yards (J.Alexander). Green Bay challenged the play for possible offensive pass interference, and the play was Upheld. The ruling on the field stands. (Timeout #2.)	14:25	CHI	CHI 42	pass	1	0	58	20	10	f	f	f	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	t	f	f	f	f	CHI	home	3	GB	7	\N	23	15	short	right	15	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	upheld	t	GB	3	1	3	1
128	3413	2019090500	1	CHI	3	GB	7	4	824	824	824	2019-09-05	Half2	(13:44) (Shotgun) M.Trubisky pass incomplete deep left to T.Gabriel. PENALTY on CHI-C.Leno, Offensive Holding, 10 yards, enforced at GB 43 - No Play.	13:44	GB	GB 43	no_play	1	0	43	20	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	7	\N	23	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	CHI	Offensive Holding	10	\N	f	\N	3	1	3	1
129	3486	2019090500	1	CHI	3	GB	7	4	815	815	815	2019-09-05	Half2	(13:35) (Shotgun) M.Trubisky pass short middle to T.Cohen to GB 48 for 5 yards (T.Williams). PENALTY on CHI-C.Leno, Illegal Use of Hands, 10 yards, enforced at CHI 47 - No Play.	13:35	CHI	CHI 47	no_play	1	0	53	20	20	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	7	\N	23	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	CHI	Illegal Use of Hands	10	\N	f	\N	3	1	3	1
130	3521	2019090500	1	CHI	3	GB	7	4	793	793	793	2019-09-05	Half2	(13:13) (Shotgun) M.Trubisky pass deep right to T.Gabriel to GB 13 for 50 yards (T.Williams). PENALTY on CHI-T.Gabriel, Offensive Pass Interference, 10 yards, enforced at CHI 37 - No Play.	13:13	CHI	CHI 37	no_play	1	0	63	20	30	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	7	\N	23	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	CHI	Offensive Pass Interference	10	\N	f	\N	3	1	3	1
131	3556	2019090500	1	CHI	3	GB	7	4	759	759	759	2019-09-05	Half2	(12:39) (Shotgun) M.Trubisky pass incomplete short right to T.Cohen.	12:39	CHI	CHI 27	pass	1	0	73	20	40	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	23	0	short	right	4	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
132	3578	2019090500	1	CHI	3	GB	7	4	753	753	753	2019-09-05	Half2	(12:33) (Shotgun) M.Trubisky pass incomplete deep middle to A.Robinson.	12:33	CHI	CHI 27	pass	2	0	73	20	40	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	23	0	deep	middle	17	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
133	3600	2019090500	1	CHI	3	GB	7	4	748	748	748	2019-09-05	Half2	(12:28) (Shotgun) M.Trubisky pass short right to T.Gabriel to CHI 36 for 9 yards (M.Adams).	12:28	CHI	CHI 27	pass	3	0	73	20	40	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	7	\N	23	9	short	right	-2	11	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
134	3624	2019090500	1	CHI	3	GB	7	4	715	715	715	2019-09-05	Half2	(11:55) (Punt formation) P.O'Donnell punts 53 yards to GB 11, Center-P.Scales, fair catch by T.Davis. PENALTY on GB-T.Brown, Illegal Blindside Block, 5 yards, enforced at GB 11.	11:55	CHI	CHI 36	punt	4	0	64	20	31	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	CHI	home	3	GB	7	\N	23	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	53	GB	Illegal Blindside Block	5	\N	f	\N	3	1	3	1
135	3680	2019090500	1	CHI	3	GB	7	4	708	708	708	2019-09-05	Half2	(11:48) A.Jones left tackle to GB 7 for 1 yard (H.Clinton-Dix).	11:48	GB	GB 6	run	1	0	94	21	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	1	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
136	3701	2019090500	1	CHI	3	GB	7	4	667	667	667	2019-09-05	Half2	(11:07) A.Rodgers pass deep right to T.Davis to GB 35 for 28 yards (H.Clinton-Dix).	11:07	GB	GB 7	pass	2	0	93	21	9	f	f	f	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	28	deep	right	18	10	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
137	3725	2019090500	1	CHI	3	GB	7	4	623	623	623	2019-09-05	Half2	(10:23) A.Jones right tackle to GB 37 for 2 yards (K.Mack).	10:23	GB	GB 35	run	1	0	65	21	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	2	\N	\N	\N	\N	right	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
138	3746	2019090500	1	CHI	3	GB	7	4	576	576	576	2019-09-05	Half2	(9:36) PENALTY on GB, Delay of Game, 5 yards, enforced at GB 37 - No Play.	09:36	GB	GB 37	no_play	2	0	63	21	8	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	GB	away	7	CHI	3	\N	73	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	GB	Delay of Game	5	\N	f	\N	3	1	1	3
139	3769	2019090500	1	CHI	3	GB	7	4	555	555	555	2019-09-05	Half2	(9:15) (Shotgun) A.Rodgers pass deep middle to R.Tonyan to CHI 40 for 28 yards (K.Fuller).	09:15	GB	GB 32	pass	2	0	68	21	13	f	f	t	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	28	deep	middle	19	9	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
140	3793	2019090500	1	CHI	3	GB	7	4	511	511	511	2019-09-05	Half2	(8:31) (No Huddle) A.Rodgers pass incomplete short left to M.Lewis.	08:31	CHI	CHI 40	pass	1	0	40	21	10	f	f	f	t	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	0	short	left	15	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
141	3815	2019090500	1	CHI	3	GB	7	4	502	502	502	2019-09-05	Half2	(8:22) A.Jones left tackle to CHI 34 for 6 yards (H.Clinton-Dix).	08:22	CHI	CHI 40	run	2	0	40	21	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	6	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
142	3836	2019090500	1	CHI	3	GB	7	4	481	481	481	2019-09-05	Half2	(8:01) A.Rodgers sacked at CHI 39 for -5 yards (L.Floyd). PENALTY on CHI-K.Fuller, Illegal Contact, 5 yards, enforced at CHI 34 - No Play.	08:01	CHI	CHI 34	no_play	3	0	34	21	4	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	GB	away	7	CHI	3	\N	73	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	CHI	Illegal Contact	5	\N	f	\N	3	1	1	3
143	3866	2019090500	1	CHI	3	GB	7	4	452	452	452	2019-09-05	Half2	(7:32) (Shotgun) J.Williams right guard to CHI 29 for no gain (E.Goldman).	07:32	CHI	CHI 29	run	1	0	29	21	10	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	0	\N	\N	\N	\N	right	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
144	3887	2019090500	1	CHI	3	GB	7	4	409	409	409	2019-09-05	Half2	(6:49) J.Williams left tackle to CHI 31 for -2 yards (R.Smith).	06:49	CHI	CHI 29	run	2	0	29	21	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	-2	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
145	3908	2019090500	1	CHI	3	GB	7	4	365	365	365	2019-09-05	Half2	(6:05) (No Huddle, Shotgun) A.Rodgers scrambles left guard to CHI 21 for 10 yards (L.Floyd).	06:05	CHI	CHI 31	run	3	0	31	21	12	f	f	t	t	t	f	f	t	f	f	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	10	\N	\N	\N	\N	left	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
146	3929	2019090500	1	CHI	3	GB	10	4	320	320	320	2019-09-05	Half2	(5:20) (Field Goal formation) M.Crosby 39 yard field goal is GOOD, Center-H.Bradley, Holder-J.Scott.	05:20	CHI	CHI 21	field_goal	4	0	21	21	2	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	GB	away	7	CHI	3	\N	73	0	\N	\N	\N	\N	\N	\N	made	\N	\N	\N	0	39	\N	\N	\N	\N	f	\N	3	1	1	3
147	3963	2019090500	1	CHI	3	GB	10	4	315	315	315	2019-09-05	Half2	M.Crosby kicks 65 yards from GB 35 to end zone, Touchback.	05:15	GB	GB 35	kickoff	\N	0	35	22	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
148	3984	2019090500	1	CHI	3	GB	10	4	315	315	315	2019-09-05	Half2	(5:15) (Shotgun) M.Trubisky pass short middle to T.Cohen to CHI 31 for 6 yards (B.Martinez).	05:15	CHI	CHI 25	pass	1	0	75	22	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	6	short	middle	4	2	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
149	4008	2019090500	1	CHI	3	GB	10	4	298	298	298	2019-09-05	Half2	(4:58) (No Huddle, Shotgun) M.Trubisky pass short middle to A.Robinson to CHI 35 for 4 yards (D.Savage).	04:58	CHI	CHI 31	pass	2	0	69	22	4	f	f	t	t	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	4	short	middle	4	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
150	4032	2019090500	1	CHI	3	GB	10	4	273	273	273	2019-09-05	Half2	(4:33) (No Huddle, Shotgun) M.Trubisky pass short right to M.Davis pushed ob at CHI 40 for 5 yards (R.Greene).	04:33	CHI	CHI 35	pass	1	0	65	22	10	f	f	t	t	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	5	short	right	0	5	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
151	4061	2019090500	1	CHI	3	GB	10	4	266	266	266	2019-09-05	Half2	(4:26) (Shotgun) M.Trubisky pass short left to T.Cohen pushed ob at CHI 49 for 9 yards (A.Amos).	04:26	CHI	CHI 40	pass	2	0	60	22	5	f	f	t	f	t	f	f	f	f	t	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	9	short	left	-2	11	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
152	4090	2019090500	1	CHI	3	GB	10	4	260	260	260	2019-09-05	Half2	(4:20) (Shotgun) M.Trubisky pass incomplete short right to T.Gabriel [Z.Smith].	04:20	CHI	CHI 49	pass	1	0	51	22	10	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	0	short	right	6	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
153	4118	2019090500	1	CHI	3	GB	10	4	256	256	256	2019-09-05	Half2	(4:16) (Shotgun) M.Trubisky pass deep right to T.Gabriel ran ob at GB 27 for 24 yards. Green Bay challenged the pass completion ruling, and the play was REVERSED. (Shotgun) M.Trubisky pass incomplete deep right to T.Gabriel.	04:16	CHI	CHI 49	pass	2	0	51	22	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	CHI	home	3	GB	10	\N	59	0	deep	right	24	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	reversed	f	\N	3	1	3	1
154	4147	2019090500	1	CHI	3	GB	10	4	251	251	251	2019-09-05	Half2	(4:11) (Shotgun) M.Trubisky pass deep middle to A.Robinson to GB 30 for 21 yards (B.Martinez).	04:11	CHI	CHI 49	pass	3	0	51	22	10	f	f	t	f	t	f	f	f	f	t	f	f	t	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	21	deep	middle	16	5	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
155	4210	2019090500	1	CHI	3	GB	10	4	213	213	213	2019-09-05	Half2	(3:33) (Shotgun) M.Trubisky pass short right to M.Davis to GB 28 for 2 yards (J.Alexander).	03:33	GB	GB 30	pass	1	0	30	22	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	2	short	right	-3	5	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
156	4234	2019090500	1	CHI	3	GB	10	4	183	183	183	2019-09-05	Half2	(3:03) (Shotgun) M.Trubisky pass short middle to T.Cohen to GB 21 for 7 yards (B.Martinez).	03:03	GB	GB 28	pass	2	0	28	22	8	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	7	short	middle	6	1	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
157	4258	2019090500	1	CHI	3	GB	10	4	164	164	164	2019-09-05	Half2	(2:44) (No Huddle, Shotgun) M.Trubisky pass short middle to T.Cohen to GB 16 for 5 yards (T.Williams).	02:44	GB	GB 21	pass	3	0	21	22	1	f	f	t	t	t	f	f	f	f	t	f	f	t	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	5	short	middle	5	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
158	4282	2019090500	1	CHI	3	GB	10	4	134	134	134	2019-09-05	Half2	(2:14) (No Huddle, Shotgun) M.Trubisky pass incomplete short right to M.Davis (R.Greene) [P.Smith].	02:14	GB	GB 16	pass	1	0	16	22	10	f	f	t	t	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	0	short	right	11	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
159	4304	2019090500	1	CHI	3	GB	10	4	128	128	128	2019-09-05	Half2	(2:08) (Shotgun) M.Trubisky pass incomplete short right to A.Shaheen (J.Alexander) [D.Savage].	02:08	GB	GB 16	pass	2	0	16	22	10	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	0	short	right	6	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
160	4326	2019090500	1	CHI	3	GB	10	4	123	123	123	2019-09-05	Half2	(2:03) (Shotgun) M.Trubisky pass deep left intended for A.Robinson INTERCEPTED by A.Amos at GB -9. Touchback.	02:03	GB	GB 16	pass	3	0	16	22	10	f	f	t	f	t	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	59	0	deep	left	16	\N	\N	\N	\N	\N	\N	GB	0	\N	\N	\N	\N	\N	f	\N	3	1	3	1
161	4370	2019090500	1	CHI	3	GB	10	4	118	118	118	2019-09-05	Half2	(1:58) A.Jones left guard to GB 25 for 5 yards (K.Mack).	01:58	GB	GB 20	run	1	0	80	23	10	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	10	CHI	3	\N	7	5	\N	\N	\N	\N	left	guard	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	3	1	1	3
162	4391	2019090500	1	CHI	3	GB	10	4	113	113	113	2019-09-05	Half2	Timeout #1 by CHI at 01:53.	01:53	GB	GB 20	no_play	\N	0	80	23	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	10	CHI	3	\N	7	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	t	CHI	2	1	1	2
163	4408	2019090500	1	CHI	3	GB	10	4	113	113	113	2019-09-05	Half2	(1:53) A.Rodgers pass incomplete short right to M.Valdes-Scantling.	01:53	GB	GB 25	pass	2	0	75	23	5	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	10	CHI	3	\N	7	0	short	right	3	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	1	1	2
164	4430	2019090500	1	CHI	3	GB	10	4	110	110	110	2019-09-05	Half2	(1:50) A.Jones left tackle to GB 27 for 2 yards (A.Hicks).	01:50	GB	GB 25	run	3	0	75	23	5	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	GB	away	10	CHI	3	\N	7	2	\N	\N	\N	\N	left	tackle	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	2	1	1	2
165	4451	2019090500	1	CHI	3	GB	10	4	105	105	105	2019-09-05	Half2	Timeout #2 by CHI at 01:45.	01:45	GB	GB 25	no_play	\N	0	75	23	0	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	10	CHI	3	\N	7	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	t	CHI	1	1	1	1
166	4468	2019090500	1	CHI	3	GB	10	4	105	105	105	2019-09-05	Half2	(1:45) (Punt formation) J.Scott punts 63 yards to CHI 10, Center-H.Bradley. T.Cohen to CHI 24 for 14 yards (A.Amos). PENALTY on CHI-D.Shelley, Offensive Holding, 10 yards, enforced at CHI 24.	01:45	GB	GB 27	punt	4	0	73	23	3	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	t	f	f	f	f	t	f	f	f	f	f	GB	away	10	CHI	3	\N	7	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	CHI	14	63	CHI	Offensive Holding	10	\N	f	\N	1	1	1	1
167	4504	2019090500	1	CHI	3	GB	10	4	93	93	93	2019-09-05	Half2	(1:33) (Shotgun) M.Trubisky pass short right to M.Davis to CHI 16 for 2 yards (R.Greene).	01:33	CHI	CHI 14	pass	1	0	86	24	10	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	t	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	-9	2	short	right	2	0	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	1	1	1	1
168	4532	2019090500	1	CHI	3	GB	10	4	77	77	77	2019-09-05	Half2	(1:17) (No Huddle, Shotgun) M.Trubisky pass incomplete deep left to A.Miller.	01:17	CHI	CHI 16	pass	2	0	84	24	8	f	f	t	t	t	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	-9	0	deep	left	29	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	1	1	1	1
169	4554	2019090500	1	CHI	3	GB	10	4	72	72	72	2019-09-05	Half2	(1:12) (Shotgun) M.Trubisky pass incomplete short middle to J.Wims [Z.Smith].	01:12	CHI	CHI 16	pass	3	0	84	24	8	f	f	t	f	t	f	f	f	t	f	f	f	f	t	f	f	t	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	-9	0	short	middle	12	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	1	1	1	1
170	4576	2019090500	1	CHI	3	GB	10	4	69	69	69	2019-09-05	Half2	(1:09) (Shotgun) M.Trubisky sacked at CHI 5 for -11 yards (P.Smith).	01:09	CHI	CHI 16	pass	4	0	84	24	8	f	f	t	f	t	f	f	f	t	f	f	f	f	f	f	t	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	t	f	f	f	f	f	f	f	CHI	home	3	GB	10	\N	-9	-11	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	1	1	1	1
171	4595	2019090500	1	CHI	3	GB	10	4	62	62	62	2019-09-05	Half2	(1:02) A.Rodgers kneels to CHI 6 for -1 yards.	01:02	CHI	CHI 5	qb_kneel	1	1	5	25	5	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	10	CHI	3	\N	-2	-1	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	1	1	1	1
172	4616	2019090500	1	CHI	3	GB	10	4	28	28	28	2019-09-05	Half2	(:28) A.Rodgers kneels to CHI 7 for -1 yards.	00:28	CHI	CHI 6	qb_kneel	2	1	6	25	6	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	GB	away	10	CHI	3	\N	-2	-1	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	1	1	1	1
173	4637	2019090500	1	CHI	3	GB	10	4	0	0	0	2019-09-05	Half2	END GAME	00:00	CHI	CHI 6	\N	\N	\N	\N	25	0	t	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	f	NA	NA	\N	NA	\N	\N	-2	0	\N	\N	\N	\N	\N	\N	\N	\N	\N	\N	0	\N	\N	\N	\N	\N	f	\N	1	1	\N	\N
\.


--
-- Data for Name: stat; Type: TABLE DATA; Schema: football; Owner: postgres
--

COPY football.stat (game_id, team, score, passing_yards, sacks, punts, return_yards) FROM stdin;
2019090500	CHI	7	100	4	2	50
2019090500	GB	7	50	2	1	25
\.


--
-- Name: game game_pkey; Type: CONSTRAINT; Schema: football; Owner: postgres
--

ALTER TABLE ONLY football.game
    ADD CONSTRAINT game_pkey PRIMARY KEY (id);


--
-- Name: play play_pkey; Type: CONSTRAINT; Schema: football; Owner: postgres
--

ALTER TABLE ONLY football.play
    ADD CONSTRAINT play_pkey PRIMARY KEY (id);


--
-- Name: stat stat_pkey; Type: CONSTRAINT; Schema: football; Owner: postgres
--

ALTER TABLE ONLY football.stat
    ADD CONSTRAINT stat_pkey PRIMARY KEY (game_id, team);


--
-- Name: ix_game_week; Type: INDEX; Schema: football; Owner: postgres
--

CREATE INDEX ix_game_week ON football.game USING btree (week);


--
-- Name: ix_play_game_id; Type: INDEX; Schema: football; Owner: postgres
--

CREATE INDEX ix_play_game_id ON football.play USING btree (game_id);


--
-- Name: ix_play_week_quarter_seconds; Type: INDEX; Schema: football; Owner: postgres
--

CREATE INDEX ix_play_week_quarter_seconds ON football.play USING btree (week, quarter, quarter_seconds_remaining);
