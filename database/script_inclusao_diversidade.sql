Códigos PL/SQL com automação de Triggers


CREATE TABLE TB_DEPARTAMENTOS (
    ID_DEP NUMBER(5),
    NOME_DEP VARCHAR2(50) NOT NULL,
    META_DIVERSIDADE NUMBER(3,2),
    CONSTRAINT PK_DEPARTAMENTO PRIMARY KEY (ID_DEP),
    CONSTRAINT CK_META CHECK (META_DIVERSIDADE BETWEEN 0 AND 1)
);
 
CREATE TABLE TB_VAGAS (
    ID_VAGA NUMBER(10),
    CARGO VARCHAR2(50) NOT NULL,
    STATUS VARCHAR2(20) DEFAULT 'ABERTA',
    FLAG_AFIRMATIVA CHAR(1) DEFAULT 'N',
    FK_DEP NUMBER(5),
    CONSTRAINT PK_VAGA PRIMARY KEY (ID_VAGA),
    CONSTRAINT FK_VAGA_DEP FOREIGN KEY (FK_DEP) REFERENCES TB_DEPARTAMENTOS(ID_DEP),
    CONSTRAINT CK_VAGA_AFIRMATIVA CHECK (FLAG_AFIRMATIVA IN ('S', 'N'))
);
 
CREATE TABLE TB_COLABORADORES (
    ID_COLAB NUMBER(10),
    NOME VARCHAR2(100) NOT NULL,
    GENERO VARCHAR2(20),
    ETNIA VARCHAR2(20),
    PCD CHAR(1) DEFAULT 'N',
    FK_DEP NUMBER(5),
    CONSTRAINT PK_COLABORADOR PRIMARY KEY (ID_COLAB),
    CONSTRAINT FK_COLAB_DEP FOREIGN KEY (FK_DEP) REFERENCES TB_DEPARTAMENTOS(ID_DEP),
    CONSTRAINT CK_COLAB_PCD CHECK (PCD IN ('S', 'N'))
);
 
CREATE TABLE TB_CANDIDATOS (
    ID_CANDIDATO NUMBER(10),
    NOME VARCHAR2(100) NOT NULL,
    SCORE_DIVERSIDADE NUMBER(3,1),
    FK_VAGA NUMBER(10),
    CONSTRAINT PK_CANDIDATO PRIMARY KEY (ID_CANDIDATO),
    CONSTRAINT FK_CAND_VAGA FOREIGN KEY (FK_VAGA) REFERENCES TB_VAGAS(ID_VAGA),
    CONSTRAINT CK_SCORE CHECK (SCORE_DIVERSIDADE BETWEEN 0 AND 10)
);
 
CREATE TABLE TB_TREINAMENTOS_LOG (
    ID_LOG NUMBER(15),
    DATA_CONCLUSAO DATE DEFAULT SYSDATE,
    TIPO_TREINAMENTO VARCHAR2(50) NOT NULL,
    FK_COLAB NUMBER(10),
    CONSTRAINT PK_LOG PRIMARY KEY (ID_LOG),
    CONSTRAINT FK_LOG_COLAB FOREIGN KEY (FK_COLAB) REFERENCES TB_COLABORADORES(ID_COLAB)
);
 
-- Sequences
CREATE SEQUENCE SEQ_COLAB START WITH 1011 INCREMENT BY 1; 
CREATE SEQUENCE SEQ_TREINAMENTO_LOG START WITH 11 INCREMENT BY 1; 

-- Inserts TB_DEPARTAMENTOS
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (1, 'Tecnologia', 0.50);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (2, 'Recursos Humanos', 0.60);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (3, 'Vendas', 0.40);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (4, 'Marketing', 0.45);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (5, 'Financeiro', 0.30);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (6, 'Operações', 0.35);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (7, 'Jurídico', 0.50);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (8, 'Logística', 0.25);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (9, 'Atendimento', 0.55);
INSERT INTO TB_DEPARTAMENTOS (ID_DEP, NOME_DEP, META_DIVERSIDADE) VALUES (10, 'Inovação', 0.70);
 
-- Inserts TB_VAGAS
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (101, 'Dev Java Jr', 'ABERTA', 'S', 1);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (102, 'Analista de RH', 'ABERTA', 'N', 2);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (103, 'Gerente Comercial', 'ABERTA', 'S', 3);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (104, 'Designer UI/UX', 'ABERTA', 'S', 4);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (105, 'Analista Fiscal', 'ABERTA', 'N', 5);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (106, 'Engenheiro de Dados', 'ABERTA', 'S', 1);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (107, 'Advogado Ambiental', 'ABERTA', 'N', 7);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (108, 'Coordenador de Frota', 'ABERTA', 'N', 8);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (109, 'Social Media', 'ABERTA', 'S', 4);
INSERT INTO TB_VAGAS (ID_VAGA, CARGO, STATUS, FLAG_AFIRMATIVA, FK_DEP) VALUES (110, 'Cientista de Dados', 'ABERTA', 'S', 10);
 
-- Inserts TB_COLABORADORES
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1001, 'Maria Oliveira', 'Feminino', 'Preta', 'N', 1);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1002, 'João Santos', 'Masculino', 'Parda', 'S', 2);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1003, 'Alice Wonder', 'Feminino', 'Branca', 'N', 3);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1004, 'Roberto Carlos', 'Masculino', 'Preta', 'N', 4);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1005, 'Fernanda M.', 'Não-binário', 'Indígena', 'N', 10);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1006, 'Lucas Silva', 'Masculino', 'Branca', 'N', 6);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1007, 'Beatriz R.', 'Feminino', 'Amarela', 'S', 9);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1008, 'Ricardo T.', 'Masculino', 'Parda', 'N', 8);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1009, 'Sonia A.', 'Feminino', 'Preta', 'N', 1);
INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, GENERO, ETNIA, PCD, FK_DEP) VALUES (1010, 'Tiago F.', 'Masculino', 'Branca', 'S', 5);
 
-- Inserts TB_TREINAMENTOS_LOG
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (1, 'Viés Inconsciente', 1001, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (2, 'Cultura Inclusiva', 1002, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (3, 'Liderança Feminina', 1003, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (4, 'Ética Corporativa', 1004, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (5, 'Acessibilidade Digital', 1007, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (6, 'Comunicação Não-Violenta', 1005, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (7, 'Antirracismo no Trabalho', 1009, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (8, 'Gestão de Conflitos', 1006, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (9, 'LGPD e Diversidade', 1008, SYSDATE);
INSERT INTO TB_TREINAMENTOS_LOG (ID_LOG, TIPO_TREINAMENTO, FK_COLAB, DATA_CONCLUSAO) VALUES (10, 'Inclusão de PCDs', 1010, SYSDATE);
 
-- Inserts TB_CANDIDATOS
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (501, 'Ana Souza', 9.5, 101);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (511, 'João Teste', 6.0, 101);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (502, 'Bruno Silva', 5.0, 102);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (503, 'Carla Diaz', 8.8, 103);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (504, 'Diego Lima', 7.5, 104);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (505, 'Elena Pires', 9.0, 106);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (512, 'Marcos Vinicius', 7.2, 106);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (507, 'Gisele B.', 8.2, 109);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (509, 'Igor K.', 9.8, 110);
INSERT INTO TB_CANDIDATOS (ID_CANDIDATO, NOME, SCORE_DIVERSIDADE, FK_VAGA) VALUES (510, 'Julia M.', 7.0, 107);
 
COMMIT;
 
/*
Trigger 1
Eliminar a necessidade de cadastro manual de novos funcionários após o encerramento de um processo
seletivo e garantir que a escolha do candidato respeite critérios de Diversidade e Inclusão
baseados em mérito social (Score de Diversidade).
 
Monitoramento: O banco de dados intercepta a finalização de uma vaga.
Seleção Inteligente: O sistema realiza uma busca interna na tabela de candidatos vinculados àquela vaga específica.
Critério de Desempate: Através de uma subconsulta, a automação ordena os candidatos pelo SCORE_DIVERSIDADE (do maior para o menor).
Promoção de Dados: O candidato que ocupa o primeiro lugar no ranking é selecionado. Seus dados (Nome e Departamento) são "copiados" para a tabela definitiva de colaboradores.
Geração de Identidade: O sistema utiliza uma SEQUENCE para gerar um novo ID único para esse colaborador, garantindo a integridade do banco.
*/
 
CREATE OR REPLACE TRIGGER TRG_CONTRATACAO_DIVERSIDADE
AFTER UPDATE OF STATUS ON TB_VAGAS
FOR EACH ROW
WHEN (NEW.STATUS = 'PREENCHIDA')
DECLARE
    v_nome_candidato TB_CANDIDATOS.NOME%TYPE;
BEGIN
    SELECT NOME INTO v_nome_candidato
    FROM (
        SELECT NOME FROM TB_CANDIDATOS
        WHERE FK_VAGA = :NEW.ID_VAGA
        ORDER BY SCORE_DIVERSIDADE DESC
    )
    WHERE ROWNUM = 1;
 
    INSERT INTO TB_COLABORADORES (ID_COLAB, NOME, FK_DEP)
    VALUES (SEQ_COLAB.NEXTVAL, v_nome_candidato, :NEW.FK_DEP);
 
    DBMS_OUTPUT.PUT_LINE('Automação ESG: Contratando o candidato ' || v_nome_candidato);
 
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        DBMS_OUTPUT.PUT_LINE('Nenhum candidato encontrado para esta vaga.');
END; 
/
 
-- Teste Trigger 1
UPDATE TB_VAGAS SET STATUS = 'PREENCHIDA' WHERE ID_VAGA = 101;
COMMIT;
 
SELECT * FROM TB_COLABORADORES; 
 
SELECT c.NOME, d.NOME_DEP
FROM TB_COLABORADORES c
JOIN TB_DEPARTAMENTOS d ON c.FK_DEP = d.ID_DEP;
 
/*
Trigger 2
Priorizar automaticamente candidatos vinculados a vagas afirmativas, garantindo maior visibilidade
para perfis diversos no processo seletivo.
 
Monitoramento: O banco de dados intercepta a inserção de um novo candidato na tabela TB_CANDIDATOS.
Regra: O sistema consulta a vaga associada ao candidato. Se a vaga for afirmativa (FLAG_AFIRMATIVA = 'S'),
o SCORE_DIVERSIDADE do candidato recebe um bônus de 2 pontos.
Controle: Para respeitar a constraint CK_SCORE, o valor final do score não pode ultrapassar 10.
Integridade: Caso a vaga informada não exista, o sistema bloqueia a inserção do candidato.
*/
 
CREATE OR REPLACE TRIGGER TRG_PRIORIDADE_CANDIDATO
BEFORE INSERT ON TB_CANDIDATOS
FOR EACH ROW
DECLARE
    v_flag_afirmativa TB_VAGAS.FLAG_AFIRMATIVA%TYPE;
BEGIN
    SELECT FLAG_AFIRMATIVA
      INTO v_flag_afirmativa
      FROM TB_VAGAS
     WHERE ID_VAGA = :NEW.FK_VAGA;
 
    IF v_flag_afirmativa = 'S' THEN
        :NEW.SCORE_DIVERSIDADE := LEAST(NVL(:NEW.SCORE_DIVERSIDADE, 0) + 2, 10);
    END IF;
 
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(-20001, 'Erro: vaga não encontrada para o candidato.');
END;
/
 
-- Trigger 3
-- Garante que todo colaborador recém-contratado seja automaticamente matriculado no treinamento obrigatório de 'INTEGRAÇÃO E CULTURA INCLUSIVA'.
-- Monitoramento: O banco intercepta qualquer novo INSERT em TB_COLABORADORES.
-- Ação Automática: Cria imediatamente um registro em TB_TREINAMENTOS_LOG, vinculando o novo colaborador ao treinamento obrigatório.
-- Identidade: Utiliza uma SEQUENCE para gerar o ID único do log.
-- Exceção: Caso o INSERT no log falhe, o banco desfaz toda a operação e exibe uma mensagem de erro clara, evitando que um colaborador seja cadastrado sem a matrícula obrigatória.
 
CREATE OR REPLACE TRIGGER TRG_MATRICULA_COMPLIANCE
AFTER INSERT ON TB_COLABORADORES
FOR EACH ROW
BEGIN
    INSERT INTO TB_TREINAMENTOS_LOG (
        ID_LOG,
        FK_COLAB,
        TIPO_TREINAMENTO,
        DATA_CONCLUSAO
    ) VALUES (
        SEQ_TREINAMENTO_LOG.NEXTVAL, 
        :NEW.ID_COLAB,
        'INTEGRAÇÃO E CULTURA INCLUSIVA',
        NULL  -- Será preenchida quando o colaborador concluir o treinamento
    );
 
    DBMS_OUTPUT.PUT_LINE(
        'Automação ESG: Colaborador ' || :NEW.NOME ||
        ' matriculado automaticamente no treinamento de compliance.'
    );
 
EXCEPTION
    WHEN OTHERS THEN
        RAISE_APPLICATION_ERROR(
            -20001,
            'COMPLIANCE ERRO: Falha ao matricular o colaborador ' ||
            :NEW.NOME || ' no treinamento obrigatório. Detalhe: ' ||
            SQLERRM
        );
END; 
/
 
/*
TRIGGER 4 — Bloqueio de Vagas "Não Inclusivas"
OBJETIVO
 
Impedir a abertura de vagas não afirmativas em departamentos com percentual de diversidade
abaixo do limiar mínimo exigido pela política ESG da empresa.
 
FUNCIONAMENTO
 
Monitoramento    : O banco de dados intercepta qualquer tentativa de INSERT em TB_VAGAS antes que ela seja registrada.
Verificação      : O sistema consulta META_DIVERSIDADE do departamento vinculado à vaga em TB_DEPARTAMENTOS.
Regra de Negócio : Se FLAG_AFIRMATIVA = 'N' e META_DIVERSIDADE < 0.10, o INSERT é bloqueado.
Permissão        : Se a vaga for afirmativa ou o departamento atender ao mínimo, o INSERT é permitido normalmente.
 
TABELAS ENVOLVIDAS
 
TB_VAGAS          → tabela monitorada (evento: BEFORE INSERT)
TB_DEPARTAMENTOS  → fonte do percentual de diversidade (coluna META_DIVERSIDADE)
 
EXCEÇÃO
 
Caso o departamento não esteja cadastrado em TB_DEPARTAMENTOS, o banco bloqueia a operação
e exibe mensagem de erro clara, evitando que uma vaga seja aberta sem validação de diversidade.
*/
 
CREATE OR REPLACE TRIGGER TRG_BLOQUEIA_VAGA_NAO_INCLUSIVA
BEFORE INSERT ON TB_VAGAS
FOR EACH ROW
DECLARE
    v_meta_diversidade TB_DEPARTAMENTOS.META_DIVERSIDADE%TYPE; 
    v_limiar_bloqueio  CONSTANT NUMBER := 0.10;                
BEGIN
    -- Só age quando a vaga NÃO é afirmativa
    IF :NEW.FLAG_AFIRMATIVA = 'N' THEN
 
        -- Busca a meta de diversidade atual do departamento
        SELECT META_DIVERSIDADE                                
          INTO v_meta_diversidade
          FROM TB_DEPARTAMENTOS
         WHERE ID_DEP = :NEW.FK_DEP;                           
 
        -- Se o departamento está abaixo do limiar → bloqueia
        IF v_meta_diversidade < v_limiar_bloqueio THEN
            RAISE_APPLICATION_ERROR(
                -20001,
                'GOVERNANÇA ESG — Abertura bloqueada. ' ||
                'O departamento ' || :NEW.FK_DEP ||            
                ' possui meta de apenas ' || (v_meta_diversidade * 100) ||
                '% de diversidade (mínimo exigido: ' ||
                (v_limiar_bloqueio * 100) || '%). ' ||
                'Altere FLAG_AFIRMATIVA para "S" ou ' ||
                'solicite revisão ao comitê de D&I.'
            );
        END IF;
 
    END IF;
 
EXCEPTION
    -- Departamento não cadastrado: bloqueia por precaução
    WHEN NO_DATA_FOUND THEN
        RAISE_APPLICATION_ERROR(
            -20002,
            'GOVERNANÇA ESG — Departamento ' || :NEW.FK_DEP || 
            ' não encontrado em TB_DEPARTAMENTOS. ' ||
            'Cadastre o departamento antes de abrir vagas.'
        );
END TRG_BLOQUEIA_VAGA_NAO_INCLUSIVA; 
/
